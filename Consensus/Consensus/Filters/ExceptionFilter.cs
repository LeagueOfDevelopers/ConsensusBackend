using System;
using Consensus.Models;
using ConsensusLibrary.DebateContext.Exceptions;
using ConsensusLibrary.UserContext.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Consensus.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case UserNotFoundException exception:
                    context.Result = new BadRequestObjectResult(
                        new ErrorViewModel(
                            ErrorType.UserNotFoundException, exception.Message));
                    return;
                case UserAlreadyExistsException exception:
                    context.Result = new BadRequestObjectResult(
                        new ErrorViewModel(
                            ErrorType.UserAlreadyExistsException, exception.Message));
                    return;
                case DebateNotFoundException exception:
                    context.Result = new BadRequestObjectResult(
                        new ErrorViewModel(
                            ErrorType.DebateNotFoundException, exception.Message));
                    return;
                case AlreadyVotedException exception:
                    context.Result = new BadRequestObjectResult(
                        new ErrorViewModel(
                            ErrorType.AlreadyVotedException, exception.Message));
                    return;
                case ArgumentNullException exception:
                    context.Result = new BadRequestObjectResult(
                        new ErrorViewModel(
                            ErrorType.ArgumentNullException, exception.Message));
                    return;
                case ArgumentException exception:
                    context.Result = new BadRequestObjectResult(
                        new ErrorViewModel(
                            ErrorType.ArgumentException, exception.Message));
                    return;
                default:
                    context.Result = new ObjectResult("Unknown error occured")
                    {
                        StatusCode = 500
                    };
                    return;
            }
        }
    }
}