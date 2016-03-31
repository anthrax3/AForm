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
//    [BlockHandle("WebTxt")]
//    [BlockTag("AControl")]
//    public class TextBox : WebControlBase<WebUI.WebControls.TextBox>
//    {
//        public TextBox(string id, IContainerBlockWeb parent)
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

//        [BlockService]
//        public void ChangeText()
//        {
//            ctl.Text += "A";
//        }
//    }
//}
