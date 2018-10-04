﻿using Microsoft.AspNetCore.Mvc.Filters;
using ConsensusLibrary.UserContext.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case UserNotFoundException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
                    return;
                case UserAlreadyExistsException exception:
                    context.Result = new BadRequestObjectResult(exception.Message);
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
