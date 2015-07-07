namespace GoSmokeBackend.Dao.Sms
{
    public class SmsServiceInitializator
    {
        private string _sid;
        private readonly string _from;
        private string _token;

        public SmsServiceInitializator(string sid, string @from, string token)
        {
            _sid = sid;
            _from = @from;
            _token = token;
        }

        public string Sid
        {
            get { return _sid; }
        }

        public string From
        {
            get { return _from; }
        }

        public string Token
        {
            get { return _token; }
        }
    }
}
