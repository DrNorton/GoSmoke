using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using GoSmokeBackend.Controllers.ApiResults;
using GoSmokeBackend.Dto.Dtos;
using Microsoft.AspNet.Identity;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus.Notifications;

namespace GoSmokeBackend.Controllers.Controllers
{
    [System.Web.Http.RoutePrefix("api/push")]
    public class PushController : CustomApiController
    {
        private readonly NotificationHubClient _hub;

        public PushController(NotificationHubClient hub)
        {
            _hub = hub;
        

        }


        [System.Web.Http.Route("Register")]
        [System.Web.Http.HttpPost]

        public async Task<ApiResult> Register(RegisterPushDto registerPushDto)
        {
            string newRegistrationId = null;
            if (registerPushDto == null)
            {
                return ErrorApiResult(300, "Какая то херня пришла от тебя братиш");
            }
            var handle = registerPushDto.Handle;
            // make sure there are no existing registrations for this push handle (used for iOS and Android)
            if (handle != null)
            {
                var registrations = await _hub.GetRegistrationsByChannelAsync(handle, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    
                        await _hub.DeleteRegistrationAsync(registration);
                    
                }
            }
            
                newRegistrationId = await _hub.CreateRegistrationIdAsync();

            return SuccessApiResult(newRegistrationId);
        }

        [System.Web.Http.Route("Update")]
        [System.Web.Http.HttpPost]
        public async Task<ApiResult> Put(DeviceDto deviceUpdate)
        {
            RegistrationDescription registration = null;
            switch (deviceUpdate.Platform)
            {
                case "mpns":
                    registration = new MpnsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "wns":
                    registration = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "apns":
                    registration = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "gcm":
                    registration = new GcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            registration.RegistrationId = deviceUpdate.RegistrationId;
            

            // add check if user is allowed to add these tags
            registration.Tags = new HashSet<string>(deviceUpdate.Tags);
       

           
                await _hub.CreateOrUpdateRegistrationAsync(registration);

                return SuccessApiResult(deviceUpdate.RegistrationId);
        }

        [System.Web.Http.Route("Sent")]
        [System.Web.Http.HttpPost]
        public async Task<ApiResult> Sent(SentPushDto sentPushDto)
        {
            var userId = User.Identity.GetUserId();
            string[] userTag = new string[2];
            userTag[0] = "username:" + sentPushDto.ToTag;
            userTag[1] = "from:" + userId;

            Microsoft.ServiceBus.Notifications.NotificationOutcome outcome = null;
            HttpStatusCode ret = HttpStatusCode.InternalServerError;

            switch (sentPushDto.Pns.ToLower())
            {
                case "wns":
                    // Windows 8.1 / Windows Phone 8.1
                    var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
                                "From " + userId + ": " + sentPushDto.Message + "</text></binding></visual></toast>";
                    outcome = await _hub.SendWindowsNativeNotificationAsync(toast);
                    break;
                case "apns":
                    // iOS
                    var alert = "{\"aps\":{\"alert\":\"" + "From " + userId + ": " + sentPushDto.Message + "\"}}";
                    outcome = await _hub.SendAppleNativeNotificationAsync(alert, userTag);
                    break;
                case "gcm":
                    // Android
                    var notif = "{ \"data\" : {\"message\":\"" + "From " + userId + ": " + sentPushDto.Message + "\"}}";
                    outcome = await _hub.SendGcmNativeNotificationAsync(notif, userTag);
                    break;
            }

            if (outcome != null)
            {
                if (!((outcome.State == Microsoft.ServiceBus.Notifications.NotificationOutcomeState.Abandoned) ||
                    (outcome.State == Microsoft.ServiceBus.Notifications.NotificationOutcomeState.Unknown)))
                {
                    ret = HttpStatusCode.OK;
                }
            }

            return SuccessApiResult(ret);
        }

    }
}
