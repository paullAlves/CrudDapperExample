using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebMotors.Anuncio.External.Model;
using WebMotors.Anuncio.External.ViewModel;

namespace WebMotors.Anuncio.External.Impl
{
    public class DefaultCarsClient : ICarsClient
    {
        private HttpClient _httpClient;
        private string BaseAddress =  "http://desafioonline.webmotors.com.br/api/OnlineChallenge";

        public IWebProxy Proxy { get; set; }

        private HttpClient GetHttpClient()
        {

            HttpClientHandler clientHandler = new HttpClientHandler();

            if (this.Proxy != null)
            {
                clientHandler.Proxy = this.Proxy;
                clientHandler.UseProxy = true;
            }
            else
            {
                clientHandler.UseProxy = false;
            }

            HttpClient httpClient = new HttpClient(clientHandler);

            httpClient.Timeout = TimeSpan.FromSeconds(30);  //RequestTimeout;
            httpClient.BaseAddress = new Uri(this.BaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;

            return _httpClient;
        }

        public async Task<MarcasResponseModel> MarcaAsync(MarcasRequestModel request, CancellationToken cancellationToken)
        {
            BaseAddress += "/Make";
            HttpClient client = GetHttpClient();
            try
            {
                HttpResponseMessage result = client.GetAsync(BaseAddress,cancellationToken).Result;
                string responseStr = await result.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                return new MarcasResponseModel { data = JsonConvert.DeserializeObject<IList<Marca>>(responseStr), ResponseStatus = result.StatusCode };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ModeloResponseModel> ModeloAsync(ModeloRequestModel request, CancellationToken cancellationToken)
        {
            BaseAddress += string.Concat("/Model?MakeID=", request.MarcaID);
            HttpClient client = GetHttpClient();
            try
            {
                HttpResponseMessage result = client.GetAsync(BaseAddress, cancellationToken).Result;
                string responseStr = await result.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                return new ModeloResponseModel { data = JsonConvert.DeserializeObject<IList<Modelo>>(responseStr), ResponseStatus = result.StatusCode };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VersaoResponseModel> VersaoAsync(VersaoRequestModel request, CancellationToken cancellationToken)
        {
            BaseAddress += string.Concat("/Version?ModelID=", request.ModeloID);
            HttpClient client = GetHttpClient();
            try
            {
                HttpResponseMessage result = client.GetAsync(BaseAddress, cancellationToken).Result;
                string responseStr = await result.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                return new VersaoResponseModel { data = JsonConvert.DeserializeObject<IList<Versao>>(responseStr), ResponseStatus = result.StatusCode };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VeiculoResponseModel> VeiculoAsync(VeiculoRequestModel request, CancellationToken cancellationToken)
        {
            BaseAddress += string.Concat("/Vehicles?Page=", request.Pagina);
            HttpClient client = GetHttpClient();
            try
            {
                HttpResponseMessage result = client.GetAsync(BaseAddress, cancellationToken).Result;
                string responseStr = await result.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                return new VeiculoResponseModel { data = JsonConvert.DeserializeObject<IList<Veiculo>>(responseStr), ResponseStatus = result.StatusCode };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
