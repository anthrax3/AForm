using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AForm.Schema
{
    public interface ISchemaElement
    {
        void WriteSchema(XmlWriter writer);
        void ReadSchema(XmlReader reader);
    }
}
