using BridgeIntServices.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BridgeIntServices.Core.Interfaces
{
    public interface IResponseHelper
    {
        ObjectResult CreateApiError(Exception ex);
        ResponseObject SuccessResponse(string message, dynamic resData);
        ResponseObject NotFoundResponse(string message, dynamic resData = null);
    }
}
