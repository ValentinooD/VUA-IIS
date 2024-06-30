using DAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DAL.Service
{
    public class SoapService : ISoapService
    {
        private IWordRepository repository = new WordRepository();

        public XElement HandleSoapRequest(XElement request)
        {
            return HandleSoapRequest(request, null);
        }

        public XElement HandleSoapRequest(XElement request, string? XPath)
        {
            var wordElement = request.XPathSelectElement("//word");
            var word = wordElement?.Value;

            if (string.IsNullOrWhiteSpace(word))
            {
                return new XElement("error", "No search term provided.");
            }

            var filtered = repository.GetWords()
                .Where(w => w.String.ToLower().Contains(word.ToLower()))
                .ToList();

            var result = new XElement("Words");

            foreach (var entity in filtered)
            {
                var elem = new XElement("Word",
                    new XElement("String", entity.String),
                    new XElement("Meaning", entity.Meaning),
                    new XElement("Invented", entity.Invented)
                );
                result.Add(elem);
            }

            if (XPath != null)
            {
                var selectedElements = result.XPathSelectElements(XPath);
                var selectedResult = new XElement("Words");

                foreach (var selectedElement in selectedElements)
                {
                    selectedResult.Add(selectedElement);
                }
                result = selectedResult;
            }

            try
            {
                string filePath = Directory.GetCurrentDirectory() + "/files/";
                int num = Directory.GetFiles(filePath).Count() + 1;
                string fullFileName = $"{filePath}/{word}_{num}.txt";
                result.Save(fullFileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
