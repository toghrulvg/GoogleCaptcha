using GoogleRecaptcha.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using static System.Net.WebRequestMethods;

namespace GoogleRecaptcha.Services
{
    public class GoogleCaptchaService
    {
        private readonly IOptionsMonitor<GoogleCaptureConfig> _config;

        public GoogleCaptchaService(IOptionsMonitor<GoogleCaptureConfig> config)
        {
            _config = config;
        }
        public async Task<bool> VerfiyToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.SecretKey}&response={token}";

                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if(httpResult.StatusCode != HttpStatusCode.OK) { return false; }

                    var responseString = await httpResult.Content.ReadAsStringAsync();

                    var googleResult = JsonConvert.DeserializeObject<GoogleCaptureResponse>(responseString);

                    return googleResult.Success && googleResult.Score >= 0.5;



                }



            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
