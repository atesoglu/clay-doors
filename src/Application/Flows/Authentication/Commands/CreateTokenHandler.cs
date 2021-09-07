using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Events;
using Application.Exceptions;
using Application.Models.Authentication;
using Application.Persistence;
using Application.Request;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ValidationException = Application.Exceptions.ValidationException;

namespace Application.Flows.Authentication.Commands
{
    /// <summary>
    /// Handler for CreateTokenCommmand
    /// </summary>
    public class CreateTokenHandler : IRequestHandler<CreateTokenCommand, TokenObjectModel>
    {
        private readonly TokenConfigModel _tokenConfig;
        private readonly IDataContext _dbContext;
        private readonly IValidator<CreateTokenCommand> _validator;
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly ILogger<CreateTokenHandler> _logger;

        /// <summary>
        /// Creates a new instance of CreateTokenHandler
        /// </summary>
        /// <param name="tokenConfig">Token configuration</param>
        /// <param name="dbContext">IDbContext implementation</param>
        /// <param name="validator">Request object validator</param>
        /// <param name="eventDispatcherService">Event dispatcher service to notify 3rd parties</param>
        /// <param name="logger">Logger implementation</param>
        public CreateTokenHandler(TokenConfigModel tokenConfig, IDataContext dbContext, IValidator<CreateTokenCommand> validator, IEventDispatcherService eventDispatcherService, ILogger<CreateTokenHandler> logger)
        {
            _tokenConfig = tokenConfig;
            _dbContext = dbContext;
            _validator = validator;
            _eventDispatcherService = eventDispatcherService;
            _logger = logger;
        }

        /// <summary>
        /// Validates the CreateTokenCommand and if successful returns a TokenObjectModel 
        /// </summary>
        /// <param name="request">CreateTokenCommand to be handled.</param>
        /// <param name="cancellationToken">Cancellation token to event to be cancelled.</param>
        /// <returns>If successful returns a TokenObjectModel</returns>
        /// <exception cref="ValidationException">If CreateTokenCommand validation fails, ValidationException is thrown</exception>
        /// <exception cref="NotFoundException">If User does not exists or credential validation fails, NotFoundException is thrown</exception>
        public async Task<TokenObjectModel> HandleAsync(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var user = await _dbContext.Users.FirstOrDefaultAsync(w => w.Email == request.Email, cancellationToken);
            if (user == null || user.PasswordHash != HashPassword(user.Salt, request.Password))
                throw new NotFoundException( /*"Can not find user."*/);

            var objectModel = new TokenObjectModel
            {
                Email = user.Email,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                    issuer: _tokenConfig.Issuer,
                    audience: _tokenConfig.Audience,
                    notBefore: DateTime.Now,
                    claims: new[]
                    {
                        new Claim("id", user.UserId.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Actor, user.Email)
                    },
                    expires: DateTime.Now.AddMinutes(_tokenConfig.AccessTokenExpiration),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret)), SecurityAlgorithms.HmacSha256))),
                Expires = _tokenConfig.AccessTokenExpiration * 60,
            };

            await _eventDispatcherService.Dispatch(new TokenCreatedEvent(objectModel, request.RequestedAt), cancellationToken);

            return objectModel;
        }

        /// <summary>
        /// Hash password with salt and return encrypted value.
        /// </summary>
        /// <param name="salt">Salt to be used in encryption</param>
        /// <param name="password">User's password</param>
        /// <returns>Encrypted value</returns>
        private static string HashPassword(string salt, string password)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 10 iterations, increase to 100000 for production) 
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: password, salt: Encoding.UTF8.GetBytes(salt), prf: KeyDerivationPrf.HMACSHA256, iterationCount: 10, numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}