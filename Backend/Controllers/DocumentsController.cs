using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        IDocuments docs = null;
        DocumentsSettings docSetting = null;

        public DocumentsController(IDocuments docRepo, IOptions<DocumentsSettings> options)
        {
            docs = docRepo;
            docSetting = options.Value;
        }

        [HttpPost]
        public BrvResult PopulateExcel([FromBody]BrvDetails brvDetails)
        {
            string fileToSave = docSetting.SavePathDocs + "[" + brvDetails.IdImpulse + "] BRV.xlsx";

            return docs.PopulateExcel(brvDetails, fileToSave);
        }

        [HttpPost("docs/{idImpulse}")]
        public async Task<DocumentDetails> SaveDocuments([FromForm(Name ="file")] IFormFile file, string idImpulse)
        {
            var newTempName = "Test Results";

            return await docs.SaveDocs(file, idImpulse, newTempName, docSetting.SavePathDocs);
        }
    }
}
