using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.V2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(ApiVersion apiVersion)
        {
            #region 引数のNULLチェック
            if (apiVersion == null)
            {
                throw new ArgumentNullException(nameof(apiVersion));
            }
            #endregion

            return await Task.Run(() =>
            {
                return this.Ok(
                    new
                    {
                        Controller = this.GetType().Name,
                        OS = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim(),
                    });
            }).ConfigureAwait(false);
        }
    }
}
