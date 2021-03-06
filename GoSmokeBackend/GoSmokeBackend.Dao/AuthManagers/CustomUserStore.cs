﻿using System;
using System.Threading.Tasks;
using GoSmokeBackend.Dao.Repositories;
using GoSmokeBackend.Dto.AuthUsers;
using Microsoft.AspNet.Identity;

namespace GoSmokeBackend.Dao.AuthManagers
{
    public class CustomUserStore : IUserStore<ApplicationUser,long>,
        IUserPasswordStore<ApplicationUser,long>,
        IUserSecurityStampStore<ApplicationUser,long>,
        IUserPhoneNumberStore<ApplicationUser,long>
    {
        private readonly IAuthRepository _repository;

        public CustomUserStore(IAuthRepository repository)
        {
            _repository = repository;
        
        }

        public Task CreateAsync(ApplicationUser user)
        {
            return _repository.RegisterUser(user);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(long userId)
        {
            return _repository.FindUser(userId);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return  _repository.FindUser(userName, "");
        }

        public Task<ApplicationUser> FindByVkIdAsync(long vkId)
        {
            return _repository.FindByVkId(vkId);
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            return _repository.UpdateUser(user);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.PasswordHash);

        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            return Task.FromResult(user.PasswordHash = passwordHash);
        }

    
    

        public Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
          
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp)
        {
            return Task.FromResult(user.SecurityStamp=stamp);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.UserName);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user)
        {
           if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.ConfirmPhone);
        }

        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            return Task.FromResult(user.ConfirmPhone = confirmed);
        }
    }
}
