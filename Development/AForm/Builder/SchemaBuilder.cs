using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Attributes;
using AForm.Schema;
using DBML.Provider;
using DBML.Interface.Provider;
using DCRF.Common.Primitive;
using DCRF.Common.Core;
using DCRF.Common.Interface;
using System.IO;
using System.Xml.Serialization;

namespace AForm.Builder
{
    [BlockId("SchemaBuilder")]
    public class SchemaBuilder: BlockBase
    {
        public SchemaBuilder(Guid id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        [BlockService]
        public FormSchema LoadFromFile(string fileName)
        {
            return FormSchema.ReadForm(fileName);
        }

        [BlockService]
        public FormSchema Build(IProvider provider, string tblName)
        {
            FormSchema schema = new FormSchema(CID.New("WinForm"));
            schema.Name = tblName;

            ControlSchema feeder = new ControlSchema(CID.New("RowFeeder"));
            schema.Controls.Add("Feeder1", feeder);
            
            
            DBField[] fields = provider.GetFieldsInfo(tblName);

            foreach (DBField field in fields)
            {
                ControlSchema ctl = new ControlSchema(CID.New("TextBox"));
                ctl.Properties.Add("Text", field.name);
                ctl.Bindings.Add("Text", new BindingSchema(field.name));
                schema.Controls.Add(field.name, ctl);

                if (field.name == "PK_ID")
                {
                    ctl.AddServiceEventHandler("TextChanged", "Feeder1", "FeedRowStr");
                }
            }

            ControlSchema req = new ControlSchema(CID.New("RequiredFieldValidator"));
            req.Bindings.Add("FieldToValidate", new BindingSchema(fields[0].name));

            ControlSchema reqLabel = new ControlSchema(CID.New("Label"));
            reqLabel.Properties.Add("Text", "id is required");
            req.Controls.Add("ReqLbl1", reqLabel);

            schema.Controls.Add("Req1", req);

            
            ControlSchema saver = new ControlSchema(CID.New("DataSaver"));
            schema.Controls.Add("Saver1", saver);

            ControlSchema ok = new ControlSchema(CID.New("Button"));
            ok.Properties.Add("Text", "Save");

            ok.AddConnectorEventHandler("Click", null, "FormSubmit"); //null means form
            ok.AddServiceEventHandler("Click","Saver1", "SaveData");

            schema.Controls.Add("OKButton", ok);

            ControlSchema grid = new ControlSchema(CID.New("DataGrid"));
            grid.Properties.Add("TableName", "TABLE1");
            grid.Properties.Add("Columns", new string[] { "PK_ID", "USER_NAME", "USER_SCORE" });
            grid.Properties.Add("ColumnHeaders", new string[] { "ID", "User Name", "User Score" });
            schema.Controls.Add("Grid1", grid);

            //schema.WriteForm("form.xml");

            //XmlSerializer xml = new XmlSerializer(typeof(FormSchema));
            //FileStream fs = new FileStream("a.xml", FileMode.Create);
            //xml.Serialize(fs, schema);

            return schema;
        }
    }
}
