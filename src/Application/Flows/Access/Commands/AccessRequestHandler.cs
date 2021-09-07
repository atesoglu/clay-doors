using System.Threading;
using System.Threading.Tasks;
using Application.Events;
using Application.Exceptions;
using Application.Models.Access;
using Application.Persistence;
using Application.Request;
using Application.Services;
using Domain.Models.Access;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ValidationException = Application.Exceptions.ValidationException;

namespace Application.Flows.Access.Commands
{
    /// <summary>
    /// Handler for AccessRequestCommand
    /// </summary>
    public class AccessRequestHandler : IRequestHandler<AccessRequestCommand, AccessGrantObjectModel>
    {
        private readonly IDataContext _dbContext;
        private readonly IValidator<AccessRequestCommand> _validator;
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly ILogger<AccessRequestHandler> _logger;

        /// <summary>
        /// Creates a new instance of AccessRequestHandler
        /// </summary>
        /// <param name="dbContext">IDbContext implementation</param>
        /// <param name="validator">Request object validator</param>
        /// <param name="eventDispatcherService">Event dispatcher service to notify 3rd parties</param>
        /// <param name="logger">Logger implementation</param>
        public AccessRequestHandler(IDataContext dbContext, IValidator<AccessRequestCommand> validator, IEventDispatcherService eventDispatcherService, ILogger<AccessRequestHandler> logger)
        {
            _dbContext = dbContext;
            _validator = validator;
            _eventDispatcherService = eventDispatcherService;
            _logger = logger;
        }

        /// <summary>
        /// Validates the AccessRequestCommand and if successful returns a AccessGrantModel 
        /// </summary>
        /// <param name="request">AccessRequestCommand to be handled.</param>
        /// <param name="cancellationToken">Cancellation token to event to be cancelled.</param>
        /// <returns>If successful returns a AccessGrantModel</returns>
        /// <exception cref="ValidationException">If AccessRequestCommand validation fails, ValidationException is thrown</exception>
        /// <exception cref="NotFoundException">If user or checkpoint does not exists, NotFoundException is thrown</exception>
        public async Task<AccessGrantObjectModel> HandleAsync(AccessRequestCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var user = await _dbContext.Users.FirstOrDefaultAsync(w => w.Email == request.Email, cancellationToken);
            if (user == null) throw new UnauthorizedException( /*$"CheckPoint is already recorded in database with the CheckPointId: {model.CheckPointId}."*/);

            var checkpoint = await _dbContext.CheckPoints.FirstOrDefaultAsync(w => w.Address == request.Address, cancellationToken);
            if (checkpoint == null) throw new UnauthorizedException( /*$"CheckPoint is already recorded in database with the CheckPointId: {model.CheckPointId}."*/);

            var accessGrant = await _dbContext.AccessGrants.FirstOrDefaultAsync(w => w.UserId == user.UserId && w.CheckPointId == checkpoint.CheckPointId, cancellationToken);
            if (accessGrant == null) throw new UnauthorizedException( /*$"CheckPoint is already recorded in database with the CheckPointId: {model.CheckPointId}."*/);

            var objectModel = new AccessGrantObjectModel(accessGrant);
            await _eventDispatcherService.Dispatch(new AccessGrantCreatedEvent(objectModel, request.RequestedAt), cancellationToken);

            return objectModel;
        }
    }
}