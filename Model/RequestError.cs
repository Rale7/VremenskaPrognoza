using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace VremenskaPrognoza.Model
{
    
    public class RequestError : Exception
    {
        private static XmlSerializer errorSerializer;

        static RequestError()
        {
            
                errorSerializer = new XmlSerializer(typeof(ErrorInfo));
            
        }

        public RequestError(string message)
        {
            using (StringReader reader = new StringReader(message))
            {
                this.MyErrorInfo = (ErrorInfo)errorSerializer.Deserialize(reader);
            }
        }

        [XmlRoot("error")]
        public class ErrorInfo
        {
            [XmlElement("code")]
            public int ErrorCode { get; set; }

            [XmlElement("message")]
            public String ErrorMessage { get; set; }
        }
       

        public ErrorInfo MyErrorInfo {  get; set; }
    }
}
