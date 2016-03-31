using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Primitive;
using System.Xml.Serialization;
using System.Xml;

namespace AForm.Schema
{
    /// <summary>
    /// ControlInfo, ControlDescriptor, 
    /// </summary>
    public class ControlSchema: ISchemaElement
    {
        public ControlSchema() { }

        public ControlSchema(CID type)
        {
            ControlType = type;
        }

        public void AddServiceEventHandler(string eventKey, string controlName, string serviceName)
        {
            if (!EventHandlers.ContainsKey(eventKey)) EventHandlers.Add(eventKey, new EventHandlerCollection());

            EventHandlerSchema ehs = new EventHandlerSchema();
            ehs.ControlName = controlName;
            ehs.ServiceName = serviceName;

            EventHandlers[eventKey].Add(ehs);
        }

        public void AddConnectorEventHandler(string eventKey, string controlName, string connectorName)
        {
            if (!EventHandlers.ContainsKey(eventKey)) EventHandlers.Add(eventKey, new EventHandlerCollection());

            EventHandlerSchema ehs = new EventHandlerSchema();
            ehs.ControlName = controlName;
            ehs.ConnectorName = connectorName;

            EventHandlers[eventKey].Add(ehs);
        }

        public CID ControlType;

        public Dictionary<string, object> Properties = new Dictionary<string, object>();
        
        //key is a property of control for example: Text in textbox or BackColor in Button
        public Dictionary<string, BindingSchema> Bindings = new Dictionary<string, BindingSchema>();
        public Dictionary<string, EventHandlerCollection> EventHandlers = new Dictionary<string, EventHandlerCollection>();
        public Dictionary<string, ControlSchema> Controls = new Dictionary<string, ControlSchema>();

        public void WriteSchema(XmlWriter writer)
        {
            XMLHelper.WriteCID(writer, ControlType);

            writer.WriteStartElement("Bindings");
            foreach (string bindingKey in Bindings.Keys)
            {
                writer.WriteStartElement("Binding");
                writer.WriteAttributeString("key", bindingKey);
                Bindings[bindingKey].WriteSchema(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Events");
            foreach (string eventKey in EventHandlers.Keys)
            {
                writer.WriteStartElement("Event");
                writer.WriteAttributeString("key", eventKey);
                EventHandlers[eventKey].WriteSchema(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Properties");
            foreach (string propertyKey in Properties.Keys)
            {
                writer.WriteStartElement("Property");
                writer.WriteAttributeString("key", propertyKey);
                XMLHelper.WriteObject(writer, Properties[propertyKey], "Value");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Controls");
            foreach (string controlKey in Controls.Keys)
            {
                writer.WriteStartElement("Control");
                writer.WriteAttributeString("key", controlKey);
                Controls[controlKey].WriteSchema(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public void ReadSchema(XmlReader reader)
        {
            ControlType = XMLHelper.ReadCID(reader);

            readBindingsSchema(reader);
            readEventsSchema(reader);
            readPropertiesSchema(reader);
            readControlsSchema(reader);
        }

        private void readControlsSchema(XmlReader reader)
        {
            XMLHelper.ProcessTagArray(reader, "Control", "Controls", new XMLHelper.TagReaderDelegate(delegate()
                {
                    string key = reader.GetAttribute("key");
                    ControlSchema cs = new ControlSchema();
                    cs.ReadSchema(reader);
                    Controls.Add(key, cs);
                }));
        }

        private void readPropertiesSchema(XmlReader reader)
        {
            XMLHelper.ProcessTagArray(reader, "Property", "Properties", new XMLHelper.TagReaderDelegate(delegate()
                {
                        string key = reader.GetAttribute("key");
                        object value = null;

                        XMLHelper.ProcessTagArray(reader, "Value", "Property", new XMLHelper.TagReaderDelegate(delegate()
                            {
                                value = XMLHelper.ReadObject(reader);
                            }));
                        Properties.Add(key, value);
                }));
        }

        private void readEventsSchema(XmlReader reader)
        {
            XMLHelper.ProcessTagArray(reader, "Event", "Events", new XMLHelper.TagReaderDelegate(delegate()
                {
                    string key = reader.GetAttribute("key");
                    EventHandlerCollection ehc = new EventHandlerCollection();
                    ehc.ReadSchema(reader);
                    EventHandlers.Add(key, ehc);
                }));
        }

        private void readBindingsSchema(XmlReader reader)
        {
            XMLHelper.ProcessTagArray(reader, "Binding", "Bindings", new XMLHelper.TagReaderDelegate(delegate()
                {
                    string key = reader.GetAttribute("key");
                    BindingSchema bs = new BindingSchema();
                    bs.ReadSchema(reader);

                    Bindings.Add(key, bs);
                }));
        }
    }
}
