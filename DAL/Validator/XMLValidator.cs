using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Commons.Xml.Relaxng;

namespace DAL.Validator
{
    public class XMLValidator
    {
        private const string RngFilePath = "./Schemas/Schema.rng";
        private const string XsdFilePath = "./Schemas/Schema.xsd";

        public SchemaType? GetSchemaType(string validationType)
        {
            if (Enum.TryParse(validationType, true, out SchemaType schemaType)) { 
                return schemaType; 
            }
            return null;
        }

        public bool Validate(XElement xmlDocument, SchemaType type)
        {
            if (type == SchemaType.RNG) return ValidateRNG(xmlDocument);
            else if (type == SchemaType.XSD) return ValidateXSD(xmlDocument);
            else return false;
        }

        private bool ValidateRNG(XElement element)
        {
            XmlReader instance = element.CreateReader();
            XmlReader schema = new XmlTextReader(RngFilePath);
            using (RelaxngValidatingReader reader = new RelaxngValidatingReader(instance, schema))
            {
                try
                {
                    while (!reader.EOF)
                    {
                        reader.Read();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("validation failed with message:");
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }

        private bool ValidateXSD(XElement xml)
        {
            try
            {
                var validationErrors = string.Empty;

                XmlDocument doc = new XmlDocument();
                doc.Load(xml.CreateReader());

                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add("", XsdFilePath);

                doc.Schemas.Add(schema);
                doc.Validate((sender, args) =>
                {
                    validationErrors += args.Message + "\n";
                });

                if (!string.IsNullOrEmpty(validationErrors))
                {
                    Console.WriteLine("XSD Validation errors are as follows:");
                    Console.WriteLine(validationErrors);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("XSD Validation exection message is:");
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
    }

    public enum SchemaType
    {
        RNG,
        XSD
    }
}
