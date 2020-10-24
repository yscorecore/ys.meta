using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YS.Meta.Rest.Api
{
    [ApiController]
    [Route("meta")]
    public class MetaController : IMetaService
    {
        private readonly IMetaService metaService;

        public MetaController(IMetaService metaService)
        {
            this.metaService = metaService;
        }
        [HttpGet]
        public Task<List<string>> GetAllKeys()
        {
            return metaService.GetAllKeys();
        }

        [HttpGet]
        [Route("{name}")]
        public Task<MetaInfo> GetMeta([FromRoute] string name)
        {
            return metaService.GetMeta(name);
        }
    }
}
