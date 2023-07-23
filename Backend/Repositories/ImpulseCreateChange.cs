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
    public class ImpulseCreateChange: IImpulseCreate
    {
        private HttpClient _httpClient;

        public ImpulseCreateChange(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ImpulseResult> CreateChg(ImpulseCreate impulse)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", GetEndcodedCredential("admin", "cCTbZ!f2pS5*"));
            var dataJson = JsonConvert.SerializeObject(impulse);
            var data = new StringContent(dataJson, Encoding.UTF8, "application/json");
            string apiUrl = "https://dev110119.service-now.com/api/sn_chg_rest/v1/change/normal";
            var response = await _httpClient.PostAsync(apiUrl, data);

            var result = response.Content.ReadAsStringAsync().Result;

            if(result != null)
            {
                var newRes = result.Replace("{\"result\":", "").Replace("}}", "}");
                var res = JsonConvert.DeserializeObject<ImpulseResult>(newRes);
                return res;
            }
            else
            {
                throw new ArgumentException("Impulse error");
            }
        }

        public static string GetEndcodedCredential(string username, string password)
        {
            string mergedCredentials = string.Format("{0}:{1}", username, password);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);

            return Convert.ToBase64String(byteCredentials);
        }
    }
}
