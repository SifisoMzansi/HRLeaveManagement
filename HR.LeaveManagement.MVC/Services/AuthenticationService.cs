﻿using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthenticationService = HR.LeaveManagement.MVC.Contracts.IAuthenticationService;

namespace HR.LeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private JwtSecurityTokenHandler _tokenHandler;
        private readonly IMapper _mapper;

        public AuthenticationService(IClient client, ILocalStorageService localStorage,
            IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(localStorage, client)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest request = new() { Email = email, Password = password };
                var AuthenticationResponse = await _client.LoginAsync(request);

                if (AuthenticationResponse.Token != string.Empty)
                {
                    var tokenContent = _tokenHandler.ReadJwtToken(AuthenticationResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                    var login = _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorageService.SetStorageValue("token", AuthenticationResponse.Token);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public async Task Logout()
        {
           _localStorageService.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(RegisterVM registration)
        {
            RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registration);
            var response = await _client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(response.UserId))
            {
                await Authenticate(registration.Email , registration.Password);
                return true;
            }
            return false;
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList() ;
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims ;
        }
    }
}
