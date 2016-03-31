//using System;
//using System.Collections.Generic;
//using System.Text;
//using AForm.Schema;
//using DCRF.Common.Attributes;
//using WebUI = System.Web.UI;
//using DCRF.Common.Core;
//using DCRF.Common.Interface;
//using DCRF.Common.Definition;
//using DCRF.Common.Primitive;

//namespace AForm.Web
//{
//    [BlockHandle("WebGen")]
//    public class WebGen : BlockBase
//    {
//        public WebGen(string id, IContainerBlockWeb parent)
//            : base(id, parent)
//        {
//        }

//        public override void InitBlock()
//        {
//            base.InitBlock();

//            innerWeb = new BlockWeb("temp", blockWeb.Broker, PlatformType.Neutral, this, false);
//        }

//        [BlockService]
//        public void GenerateForm(WebUI.Control container)
//        {
//            FormSchema frm = new FormSchema(BlockHandle.New("WebForm"));
//            frm.Name = "form1";
//            frm.Comments = "some comments";

//            ControlSchema ct = new ControlSchema(BlockHandle.New("WebLbl"));
//            ct.Properties.Add("Text", "Hello world!");
//            frm.Controls.Add("Label1", ct);

//            ControlSchema tt = new ControlSchema(BlockHandle.New("WebTxt"));
//            tt.Properties.Add("Text", "TextBox1");
//            frm.Controls.Add("TextBox1", tt);

//            ControlSchema btn = new ControlSchema(BlockHandle.New("WebBtn"));
//            btn.Properties.Add("Text", "Ok Button");
//            EventHandlerSchema eht = new EventHandlerSchema();
//            eht.ControlName = "TextBox1";
//            eht.ServiceName = "ChangeText";
//            btn.EventHandlers["Click"].Add(eht);
//            frm.Controls.Add("Button1", btn);

//            string webId = innerWeb.AddBlock(BlockHandle.New("WebForm"));
//            innerWeb[webId].ProcessRequest("InitForm", frm);
//            innerWeb[webId].ProcessRequest("Bind", container);
//        }
//    }
//}
