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
//using AForm.Web.Base;

//namespace AForm.Web.Controls
//{
//    [BlockHandle("WebLbl")]
//    [BlockTag("AControl")]
//    public class Label : WebControlBase<WebUI.WebControls.Label>
//    {
//        public Label(string id, IContainerBlockWeb parent)
//            : base(id, parent)
//        {
//        }

//        public override void InitControl()
//        {
//            base.InitControl();

//            if (myTemplate.Properties.ContainsKey("Text"))
//            {
//                ctl.Text = myTemplate.Properties["Text"].ToString();
//            }
//        }
//    }
//}
