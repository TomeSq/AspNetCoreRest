using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TodoApi
{
    public class AssignJwtSecurityRequirements : IOperationFilter
    {
        /// <summary>
        /// Swagger UI用フィルタ
        /// Swagger上でAPIを実行する際のJWTトークン認証対応を実現する
        /// </summary>
        /// <param name="operation">operation</param>
        /// <param name="context">context</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            #region 引数のNULLチェック
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            #endregion

            if (operation.Security == null)
            {
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            }

            var allowAnonymousAccess = context.MethodInfo.CustomAttributes
                .Any(a => a.AttributeType == typeof(AllowAnonymousAttribute));

            if (allowAnonymousAccess == false)
            {
                var authRequirements = new Dictionary<string, IEnumerable<string>>
                {
                    { "api_key", new List<string>() },
                };
                operation.Security.Add(authRequirements);
            }
        }
    }
}
