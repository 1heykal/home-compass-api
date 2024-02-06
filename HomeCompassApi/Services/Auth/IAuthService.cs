﻿using HomeCompassApi.Models.Auth;

namespace HomeCompassApi.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);

        Task<AuthModel> GetTokenAsync(TokenRequestModel model);

        Task<string> AddRoleAsync(AddRoleModel model);

        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
