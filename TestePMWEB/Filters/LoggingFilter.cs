using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using TestePMWEB.Models;
using TestePMWEB.Repository;

namespace TestePMWEB.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private readonly IUnitOfWork _uof;

        public LoggingFilter(IUnitOfWork uof)
        {
            _uof = uof;
        }

        //Antes da Action
        public void OnActionExecuted(ActionExecutedContext context)
        
        {
            ObjectResult obj = context.Result as ObjectResult;
            var status = obj.StatusCode;


            HttpStatusCode code = (HttpStatusCode)status;
            var message = new HttpResponseMessage(code);
            
            short result = 0;
            if (message.IsSuccessStatusCode) result = 1;
           
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var tipo = (string)context.RouteData.Values["Controller"];

            var log = new API_Log
            {
                DATA_REFERENCIA = timestamp,
                MENSAGEM = message.ReasonPhrase,
                TIPO = tipo,
                DETALHE = (int)status,
                RESULTADO = result
            };

            _uof.API_LogRepository.Add(log);
            _uof.Commit();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
