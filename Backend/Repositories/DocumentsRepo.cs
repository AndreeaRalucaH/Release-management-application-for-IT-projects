using Microsoft.AspNetCore.Http;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class DocumentsRepo : IDocuments
    {
        DocumentDetails newDoc = new DocumentDetails();

        public BrvResult PopulateExcel(BrvDetails brvDetails, string targetFile)
        {
            var brvRes = new BrvResult();
            try
            {
                var currentFilePath = Directory.GetCurrentDirectory() + "\\Component\\Validation\\BRV Document.xlsx";
                File.Copy(currentFilePath, targetFile);

                XlsFunctions.UpdateCell(targetFile, brvDetails.CreationDate, 6, "D");
                XlsFunctions.UpdateCell(targetFile, brvDetails.Author, 7, "D");
                XlsFunctions.UpdateCell(targetFile, brvDetails.Application, 8, "D");
                XlsFunctions.UpdateCell(targetFile, brvDetails.PoApp, 17, "D");
                XlsFunctions.UpdateCell(targetFile, brvDetails.ItApp, 17, "E");
                XlsFunctions.UpdateCell(targetFile, brvDetails.PoDate, 18, "D");
                XlsFunctions.UpdateCell(targetFile, brvDetails.ItDate, 18, "E");

                var jiraList = new List<Tuple<string, string, string, string, string, string>>();
                foreach(var jira in brvDetails.JiraContents)
                {
                    jira.Status = "To do";
                    jiraList.Add(new Tuple<string, string, string, string, string, string>(jira.TicketID, jira.Type, jira.Summary, jira.Requestor, jira.PrioritizationDate, jira.Status));
                }
                 
                XlsFunctions.insertCells(targetFile, jiraList, 12, "D", brvDetails.PoApp);

                brvRes.DocAdded = true;
                brvRes.FilePath = targetFile;
                return brvRes;
            }
            catch(Exception ex)
            {
                brvRes.DocAdded = false;
                brvRes.FilePath = "";
                return brvRes;
            }
        }

        public async Task<DocumentDetails> SaveDocs(IFormFile file, string idImpulse, string tempFileName, string savePath)
        {
            try
            {
                if(file.Length > 0)
                {
                    var newExtension = "";
                    var sTempFileName = "[" + idImpulse + "] " + tempFileName + " Validation";
                    var newFileName = sTempFileName + "." + file.FileName.Substring(file.FileName.Length - 4).ToString();
                    var sExtension = file.FileName.Substring(file.FileName.Length - 4).ToString();

                    if (sExtension.Contains("."))
                    {
                        newExtension = sExtension.Remove(0, 1);
                    }
                    else
                    {
                        newExtension = sExtension;
                    }

                    var filePath = Path.Combine(savePath, newFileName);

                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        newDoc.Extension = newExtension;
                        newDoc.STempFile = sTempFileName;
                        newDoc.SavePath = filePath;

                        return newDoc;
                    }
                }
                else
                {
                    return new DocumentDetails();
                }

            }catch(Exception e)
            {
                return new DocumentDetails();
            }
        }
    }
}
