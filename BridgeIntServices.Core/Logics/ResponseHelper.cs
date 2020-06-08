using BridgeIntServices.Core.Interfaces;
using BridgeIntServices.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace BridgeIntServices.Core.Logics
{
    public class ResponseHelper : IResponseHelper
    {
        private readonly IBridgeLogger _bridgeLogger;
        public ResponseHelper(IBridgeLogger bridgeLogger)
        {
            _bridgeLogger = bridgeLogger;

        }

        public ObjectResult CreateApiError(Exception ex)
        {
            _bridgeLogger.LogError(ex.Message);
            return new ObjectResult(new ResponseObject()
            {
                Status = "Error",
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                ResponseCode = new int?(500),
                Data = null
            });
        }
        public ResponseObject SuccessResponse(string message, dynamic resData)
        {
            return new ResponseObject()
            {
                Status = "Success",
                Message = message,
                StackTrace = null,
                ResponseCode = (int)HttpStatusCode.OK,
                Data = resData
            };
        }
        public ResponseObject NotFoundResponse(string message, dynamic resData = null)
        {
            return new ResponseObject()
            {
                Status = "Failure",
                Message = message,
                StackTrace = null,
                ResponseCode = (int)HttpStatusCode.NotFound,
                Data = resData
            };
        }
    }
}
