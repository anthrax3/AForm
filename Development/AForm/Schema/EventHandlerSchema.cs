using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AForm.Schema
{
    public class EventHandlerSchema : ISchemaElement
    {
        public string ControlName;
        public string ServiceName;
        public string ConnectorName;

        public void WriteSchema(XmlWriter writer)
        {
            writer.WriteStartElement("EventHandler");
            writer.WriteAttributeString("controlName", ControlName);
            writer.WriteAttributeString("serviceName", ServiceName);
            writer.WriteAttributeString("connectorName", ConnectorName);
            writer.WriteEndElement();
        }

        public void ReadSchema(XmlReader reader)
        {
            ControlName = reader.GetAttribute("controlName");
            ServiceName = reader.GetAttribute("serviceName");
            ConnectorName = reader.GetAttribute("connectorName");
        }
    }

    [Serializable]
    public class EventHandlerCollection: List<EventHandlerSchema>, ISchemaElement
    {
        public void WriteSchema(XmlWriter writer)
        {
            foreach (EventHandlerSchema item in this)
            {
                item.WriteSchema(writer);
            }
        }

        public void ReadSchema(XmlReader reader)
        {
            XMLHelper.ProcessTagArray(reader, "EventHandler", "Event", new XMLHelper.TagReaderDelegate(delegate()
                {
                    EventHandlerSchema schema = new EventHandlerSchema();
                    schema.ReadSchema(reader);
                    Add(schema);
                }));
        }
    }
}
