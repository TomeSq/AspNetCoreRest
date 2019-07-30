﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers.V1
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
                    Controller = this.GetType().Name,
                    Version = apiVersion.ToString(),
                });
        }
    }
}
