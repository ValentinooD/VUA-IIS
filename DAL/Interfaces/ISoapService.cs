using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Interfaces
{
    public interface ISoapService
    {
        XElement HandleSoapRequest(XElement request);

        XElement HandleSoapRequest(XElement request, string? XPath);
    }
}
