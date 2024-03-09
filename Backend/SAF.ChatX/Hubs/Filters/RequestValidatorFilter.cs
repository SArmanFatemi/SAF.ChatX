﻿using Microsoft.AspNetCore.SignalR;
using SAF.ChatX.Dtos.Common;

namespace SAF.ChatX.Hubs.Filters;

public class RequestValidatorFilter : IHubFilter
{
    public async ValueTask<object?> 
        InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        if (invocationContext.HubMethodArguments[0] is IValidatableRequest request)
        {
            if (request.IsValid() is false)
                throw new HubException(
                    $"INVALID REQUEST: One of the parameters for calling [${invocationContext.HubMethodName}] hub method is not valid");
        }

        return await next(invocationContext);
    }
}