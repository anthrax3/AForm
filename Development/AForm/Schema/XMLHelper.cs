using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DCRF.Common.Primitive;

namespace AForm.Schema
{
    public class XMLHelper
    {
        public delegate void TagReaderDelegate();

        /// <summary>
        /// read through XML tags until we reach either:
        /// 1 - Start of targetTag
        /// 2 - End of outerTag
        /// 3 - End of XML file
        /// While 2 or 3 have not occured, it repeats to execute tagReader 
        /// Returns false if 2 or 3 happens, true in case of 1
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="targetTag"></param>
        /// <param name="outerTag"></param>
        public static void ProcessTagArray(XmlReader reader, string targetTag, string outerTag, TagReaderDelegate tagReader)
        {
            while (true)
            {
                bool processed = ProcessTag(reader, targetTag, outerTag, tagReader);

                //exit if outerTag is seen or document is finished
                if (!processed)
                {
                    return;
                }
                else
                {
                    reader.Read();  //move to the next item so ProcessTag will continue to next tag
                }
            }
        }


        /// <summary>
        /// read through XML tags until we reach either:
        /// 1 - Start of targetTag
        /// 2 - End of outerTag or empty outerTag was read
        /// 3 - End of XML file
        /// While 2 or 3 have not occured, it repeats to execute tagReader 
        /// Returns false if 2 or 3 happens, true in case of 1
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="targetTag"></param>
        /// <param name="outerTag"></param>
        public static bool ProcessTag(XmlReader reader, string targetTag, string outerTag, TagReaderDelegate tagReader)
        {
            while (!((reader.EOF) ||
                        (reader.Name == targetTag && reader.NodeType == XmlNodeType.Element) ||
                        (outerTag != null && reader.Name == outerTag && reader.NodeType == XmlNodeType.EndElement)))
            {
                if ((outerTag != null && reader.Name == outerTag && reader.IsEmptyElement))
                {
                    return false;
                }

                reader.Read();
            }

            if (reader.Name == targetTag && reader.NodeType == XmlNodeType.Element)
            {
                //run tagReader and go for the next occurence of 'targetTag' within outerTag
                tagReader();

                return true;
            }

            return false;
        }

        public static void WriteObjectArray(XmlWriter writer, object[] data, string tag)
        {
            writer.WriteStartElement(tag);
            if (data == null)
            {
                writer.WriteAttributeString("isNull", "true");
            }
            else
            {
                foreach (object item in data)
                {
                    WriteObject(writer, item, "Item");
                }
            }
            writer.WriteEndElement();
        }

        public static void WriteObject(XmlWriter writer, object data, string tag)
        {
            writer.WriteStartElement(tag);
            
            if (data == null)
            {
                writer.WriteAttributeString("isNull", "true");
            }
            else
            {
                Type dataType = data.GetType();

                writer.WriteAttributeString("type", dataType.FullName);

                if (!dataType.IsArray)
                {
                    writer.WriteAttributeString("value", data.ToString());
                }
                else
                {
                    WriteObjectArray(writer, (object[])data, tag + "Array");
                }
            }

            writer.WriteEndElement();
        }

        public static void WriteCID(XmlWriter writer, CID cid)
        {
            writer.WriteStartElement("ControlType");
            writer.WriteAttributeString("id", cid.Identifier);
            writer.WriteAttributeString("version", cid.BlockVersion.ToString());
            writer.WriteAttributeString("product", cid.Product);
            writer.WriteEndElement();
        }


        public static object[] ReadObjectArray(XmlReader reader, string tag, string outerTag)
        {
            object[] result = null;

            ProcessTagArray(reader, tag, outerTag, new TagReaderDelegate(delegate()
                {
                    if (reader.GetAttribute("isNull") != "true")
                    {
                        List<object> list = new List<object>();

                        ProcessTagArray(reader, "Item", tag, new TagReaderDelegate(delegate()
                            {
                                object item = ReadObject(reader);
                                list.Add(item);
                            }));

                        result = list.ToArray();
                    }
                }));

            return result;
        }

        public static object ReadObject(XmlReader reader)
        {
            object result = null;

            if (reader.GetAttribute("isNull") != "true")
            {
                string type = reader.GetAttribute("type");
                string value = reader.GetAttribute("value");

                Type dataType = Type.GetType(type);

                if (!dataType.IsArray)
                {
                    result = Convert.ChangeType(value, Type.GetType(type));
                }
                else
                {
                    object[] array = ReadObjectArray(reader, reader.Name + "Array", reader.Name);
                    //Type innerType = dataType.GetElementType();

                    //for(int i=0;i<array.Length;i++)
                    //{
                    //    array[i] = Convert.ChangeType(array[i], innerType);
                    //}

                    result = Array.ConvertAll<object, string>(array, new Converter<object, string>(delegate(object obj) { return (string)obj; }));
                }

            }

            return result;
        }


        public static CID ReadCID(XmlReader reader)
        {
            CID result = new CID();

            ProcessTag(reader, "ControlType", null, new TagReaderDelegate(delegate()
                {
                    result.Identifier = reader.GetAttribute("id");
                    result.BlockVersion = new BlockVersion(reader.GetAttribute("version"));
                    result.Product = reader.GetAttribute("product");
                }));

            return result;
        }
    }
}
