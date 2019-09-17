using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Logic
{
    public static class WSRequest
    {
        static string sdsPrefix = "datasnap/rest/";

        public static async Task<HttpResponseMessage> RequestGET(string sdsUrl)
        {
            try
            {

                var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri("http://" + App.current.sdsServidorApp + ":" + App.current.nnrPorta) };

                var byteArray = Encoding.ASCII.GetBytes(App.current.sdsUsuario + ":" + App.current.sdsSenha);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

                var response = await client.GetAsync(sdsPrefix+sdsUrl);

                response.EnsureSuccessStatusCode();

                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
