using Microsoft.AspNetCore.Http;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.IRepositories
{
    public interface IDocuments
    {
        BrvResult PopulateExcel(BrvDetails brvDetails, string targetFile);
        Task<DocumentDetails> SaveDocs(IFormFile file, string idImpulse, string tempFileName, string savePath);
    }
}
