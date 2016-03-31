using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Primitive;
using System.Xml;

namespace AForm.Schema
{
    public class FormSchema : ControlSchema
    {
        public FormSchema() { }

        public FormSchema(CID type)
            : base(type)
        {
        }

        public string Name = "";
        public string Comments = "";

        public void WriteForm(string filePath)
        {
            XmlTextWriter writer = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            WriteForm(writer);
            writer.Close();
        }

        public void WriteForm(XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("Form");
            writer.WriteAttributeString("name", Name);
            writer.WriteAttributeString("comments", Comments);
            WriteSchema(writer);
            writer.WriteEndElement();
        }

        public static FormSchema ReadForm(string filePath)
        {
            XmlTextReader reader = new XmlTextReader(filePath);
            FormSchema result = ReadForm(reader);
            reader.Close();

            return result;
        }

        public static FormSchema ReadForm(XmlReader reader)
        {
            FormSchema result = new FormSchema();
            XMLHelper.ProcessTagArray(reader, "Form", null, new XMLHelper.TagReaderDelegate(delegate()
                {
                    result.Name = reader.GetAttribute("name");
                    result.Comments = reader.GetAttribute("comments");

                    result.ReadSchema(reader);
                }));

            return result;
        }
    }
}
