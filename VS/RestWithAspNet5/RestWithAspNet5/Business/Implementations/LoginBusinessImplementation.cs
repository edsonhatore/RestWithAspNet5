﻿using RestWithAspNet5.Configurations;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestWithAspNet5.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {

        private const string DATE_FORMAT = "yyyy-MM-dd HH>mm:ss";
        private TokenConfiguration _configuration;
        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredencials)
        {

            var user = _repository.ValidateCredencial(userCredencials);
            if (user == null) return null;

            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
               new Claim(JwtRegisteredClaimNames.UniqueName , user.Username)
           };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

           // _repository.RefresUserInfo(user);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes) ;

            _repository.RefresUserInfo(user);

            return new TokenVO( true,createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT),
               accessToken , refreshToken );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var  username = principal.Identity.Name;

            var user = _repository.ValidateCredencial(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            /// atualiza o user 
            _repository.RefresUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);


            return new TokenVO(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT),
               accessToken, refreshToken);


        }

        public bool RevokeToken(string username)
        {
            return _repository.RevokeToken(username);
        }
    }
}
    