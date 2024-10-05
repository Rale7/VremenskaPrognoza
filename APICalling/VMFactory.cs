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

        private XmlSerializer realtimeSerializer = new XmlSerializer(typeof(RealtimeResponse));
        private XmlSerializer astronomySerializer = new XmlSerializer(typeof(AstronomyResponse));

        private ResponseViewModel rvm = new ResponseViewModel();

        MinutChangeWatcher watcher;

        private String oldValue = "Belgrade";
        private String location = "Belgrade";

        public String Location
        {
            get { return location; }
            set 
            {
                oldValue = location;
                location = value;
                Task.Run(() => SendRequestAndUpdate());
            }
        }

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
            String response;
            try
            {
                response = await ApiClient.SendGetRequestAsync
                    (ApiClient.BaseRealtimeUrl, apiKey, location, "en");
                updateRealtimeWeatherState(response);
                response = await ApiClient.SendGetRequestAsync
                    (ApiClient.BaseAstronomyUrl, apiKey, location, "en");
                updateAstronomyState(response);
            }
            catch (RequestError ex)
            {
                location = oldValue;
                
                MessageBox.Show($"API error {ex.MyErrorInfo.ErrorCode}\n{ex.MyErrorInfo.ErrorMessage}");                
            }
        }

        private void updateRealtimeWeatherState(String xmlResponse)
        {
            using (StringReader reader = new StringReader(xmlResponse))
            {
                try
                {                    
                    RealtimeResponse result = (RealtimeResponse)
                        realtimeSerializer.Deserialize(reader);
                    Rvm.RealtimeResponse = result;
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void updateAstronomyState(String xmlResponse) 
        {
            using (StringReader reader = new StringReader(xmlResponse))
            {
                try
                {
                    AstronomyResponse result = (AstronomyResponse)
                        astronomySerializer.Deserialize(reader);
                    Rvm.AstronomyResponse = result;
                }
                catch (InvalidOperationException ex)
                {                    
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
