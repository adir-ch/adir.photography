﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;

namespace adir.photography.Controllers
{
    public class logData
    {
        public string LogData { get; set; } 
    }

    [RoutePrefix("api/clientlogging")]
    public class ClientLoggingController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger("ClientLogger");
        
        public ClientLoggingController()
        {

        }

        // POST api/clientlogging/
        [Route("")]
        [HttpPost]
        public IHttpActionResult LogClientException([FromBody] string value)
        {
            _log.ErrorFormat("Client exception: {0}", value);
            return Ok();
        }

    }
}
