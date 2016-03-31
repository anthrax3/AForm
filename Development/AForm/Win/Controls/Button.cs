using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Contract.Impl;
using DCRF.Common.Attributes;
using DCRF.Common.Interface;
using WinUI = System.Windows.Forms;
using DCRF.Common.Core;
using DCRF.Common.Definition;
using AForm.Win.Base;
using DCRF.Common.Primitive;
using System.Windows.Forms;
using DCRF.Common.Helper;
using System.Drawing;
using System.IO;
using DCRF.Common.XML;

namespace AForm.Win.Controls
{
    [BlockHandle("Button")]
    [BlockTag("AControl")]
    public class Button : WinControlBase<WinUI.Button>
    {
        private BlockEvent clickEvent = null;

        public Button(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitBlock()
        {
            base.InitBlock();

            clickEvent = new BlockEvent(this, "Click");
        }

        public override void  OnAfterLoad()
        {
            ctl.Click +=
               new EventHandler(
                   delegate(object sender, EventArgs e)
                   {
                       clickEvent.Raise();
                   }
               );

            if (HasConnector("image"))
            {
                ctl.Image = Image.FromStream(this["image"].GetValue<Stream>());
            }

            base.OnAfterLoad();
        }

        [BlockService]
        public void OpenForm(string xmlFile)
        {
            IBlockWeb bw = XMLLoader.LoadBlockWeb(xmlFile, "a", blockWeb.Broker);

            string formId = bw.GetConnector("FormId").GetValue<string>();
            Form frm = bw[formId].ProcessRequest("GetUIElement") as Form;
            frm.ShowDialog();
        }
    }
}
