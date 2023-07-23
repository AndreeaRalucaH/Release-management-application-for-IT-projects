using Newtonsoft.Json;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class JiraRepo : IJira
    {
        private HttpClient _httpClient;

        public JiraRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<JiraIssue>> GetIssue(string issue)
        {
            var listOfIssues = new List<JiraIssue>();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", GetEndcodedCredential("andreearaluca210@gmail.com", "ATATT3xFfGF0O-ocawxEaDMnxJ3RXPTx-WiGC9yO132wEQpWODGFXJopCwuQ5WlfS4YSsVOru3ae2QTeeuXs73IdXNn8NVIyzysgiLNhKt6qYIkTqEf3_Wv7fu2vXlAyJ5B65UPOmApIsPtF56B_vS_v-eqnmGx0UgMm-kttYfxKxkoAeBVq_qY=F566C8A6"));
            string[] multipleIssues = issue.Split(';', '.', ',');

            foreach(var issues in multipleIssues)
            {
                string apiUrl = "https://releasemonitor.atlassian.net/rest/api/3/issue/" + issues;
                string response = await _httpClient.GetStringAsync(apiUrl);
                if(response != null)
                {
                    var res = JsonConvert.DeserializeObject<JiraIssue>(response);
                    listOfIssues.Add(res);
                }
                else
                {
                    throw new ArgumentException("Incorrect issue");
                }
            }

            return listOfIssues;
        }

        public static string GetEndcodedCredential(string username, string password)
        {
            string mergedCredentials = string.Format("{0}:{1}", username, password);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);

            return Convert.ToBase64String(byteCredentials);
        }
    }
}
