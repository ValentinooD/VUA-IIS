using CookComputing.XmlRpc;
using DAL.Interfaces;
using System.Text;
using System.Xml;

namespace DAL.Service
{
    public class WeatherService : IWeatherService
    {
        private const string URL_RPC = @"http://localhost:8000/xmlrpc/";

        private IXmlRpcService rpc;

        public WeatherService()
        {
            this.rpc = XmlRpcProxyGen.Create<IXmlRpcService>();
            rpc.Url = URL_RPC;
        }

        public double GetWeather(string city)
        {
            return rpc.GetTemperature(city);
        }
    }

    public interface IXmlRpcService : IXmlRpcProxy
    {
        [XmlRpcMethod("handler.getTemperature")]
        double GetTemperature(string city); 
    }
}
