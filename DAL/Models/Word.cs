using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Models
{
    public class Word
    {
        public string String { get; set; } = string.Empty;
        public string Meaning { get; set; } = string.Empty;
        
        public int Invented { get; set; }
        

        public static IList<Word> GetWords(XElement element)
        {
            var words = new List<Word>();

            foreach (XElement word in element.Elements())
            {
                words.Add(new Word()
                {
                    String = word.Element("String")?.Value ?? string.Empty,
                    Meaning = word.Element("Meaning")?.Value ?? string.Empty,
                    Invented = int.Parse(word.Element("Invented")?.Value ?? "0")
                });
            }

            return words;
        }
    }
}
