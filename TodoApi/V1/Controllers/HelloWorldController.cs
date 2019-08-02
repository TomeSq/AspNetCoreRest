using System;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(ApiVersion apiVersion)
        {
            #region 引数のNULLチェック
            if (apiVersion == null)
            {
                throw new ArgumentNullException(nameof(apiVersion));
            }
            #endregion

            return this.Ok(
                new
                {
                    ApiVersion = apiVersion.ToString(),
                    OS = System.Environment.OSVersion,
                    OSName = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                });
        }
    }
}
