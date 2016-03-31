using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AForm.Schema
{
    public class BindingSchema: ISchemaElement
    {
        public BindingSchema() { }

        public BindingSchema(string key):this(null,key)
        {
        }

        public BindingSchema(string source, string key, params object[] id)
        {
            BindingKey = key;
            BindingSource = source;

            if (id.Length > 0)
            {
                BindingId = new object[id.Length];
                for (int i = 0; i < id.Length; i++)
                {
                    BindingId[i] = id[i];
                }
            }
        }

        //this is usually fieldName which contains data. for example FLD_TXT_TITLE
        public string BindingKey = null;

        //This will be used to identify the bindingContext (DataRow of a table or any similar concept in other datasources)
        //By giving bindingId to BindingSource it will be able to spot the location of data for binding operation.
        //after that, BindingKey will specify the exact data in that location.
        //BindingId is similar to Primary Key and BindingKey is similar to Field name.
        //this maybe specified in static schema or updated at runtime or null for binding to multiple rows
        public object[] BindingId = null;

        //Name of the control which will provide value for this binding. By default 'form' will be
        //data provider in which case BindingSource will be set to null.
        //The BindingSource will provide required connectors so other can processRequest to read value of connector
        //or attach endpoint to write value of that binding.
        public string BindingSource = null;

        public string GetBindingUniqueKey()
        {
            StringBuilder result = new StringBuilder(BindingKey);

            if (BindingId != null)
            {
                for(int i=0;i<BindingId.Length;i++)
                {
                    object item = BindingId[i];

                    result.Append(item.ToString());

                    if (i < BindingId.Length - 1)
                    {
                        result.Append(".");
                    }
                }
            }

            return result.ToString();
        }

        public void WriteSchema(XmlWriter writer)
        {
            writer.WriteAttributeString("bindingSource", BindingSource);
            writer.WriteAttributeString("bindingKey", BindingKey);
            XMLHelper.WriteObjectArray(writer, BindingId, "BindingId");
        }

        public void ReadSchema(System.Xml.XmlReader reader)
        {
            BindingKey = reader.GetAttribute("bindingKey");
            BindingSource = reader.GetAttribute("bindingSource");
            BindingId = XMLHelper.ReadObjectArray(reader, "BindingId", "Binding");
        }
    }
}
