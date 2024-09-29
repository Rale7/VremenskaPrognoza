using System.IO;
using System.Net.Http;
using System.Windows;
using System.Xml.Serialization;
using VremenskaPrognoza.Model;
using VremenskaPrognoza.ViewModel;

namespace VremenskaPrognoza.APICalling
{
    public class VMFactory
    {
        private static VMFactory? instance;

        public static VMFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VMFactory();
                }
                return instance;
            }
        }

        private String apiKey;

        private XmlSerializer serializer = new XmlSerializer(typeof(Response));

        private ResponseViewModel rvm = new ResponseViewModel();

        MinutChangeWatcher watcher;

        public ResponseViewModel Rvm { 
            get 
            { 
                return rvm; 
            } 
        }

        private VMFactory() {            
            // loading API key
            EnvReader.LoadEnvironmentVariables(".env");
            apiKey = Environment.GetEnvironmentVariable("API_KEY");

            Task.Run(() => SendRequestAndUpdate());
            watcher = new MinutChangeWatcher(SendRequestAndUpdate);
        }

        private async Task SendRequestAndUpdate()
        {
            try
            {
                String response = await 
                    ApiClient.SendGetRequestAsync(apiKey, "Belgrade", "en");
                updateWeatherState(response);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"API error {ex.Message}");             
            }
        }

        private void updateWeatherState(String xmlResponse)
        {
            using (StringReader reader = new StringReader(xmlResponse))
            {
                try
                {                    
                    Response result = (Response)serializer.Deserialize(reader);
                    Rvm.Response = result;
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
