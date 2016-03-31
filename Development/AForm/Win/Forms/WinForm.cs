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
using System.Drawing;
using System.IO;

namespace AForm.Win.Forms
{
    [BlockHandle("WinForm")]
    [BlockTag("AForm")]
    public class WinForm : WinFormBase<WinUI.Form>
    {
        public WinForm(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitBlock()
        {
            base.InitBlock();
        }

        [BlockService]
        public string GetTable()
        {
            return "TABLE1";
        }

        [BlockService]
        public void CloseForm()
        {
            ctl.FindForm().Close();
        }

        public override void OnAfterLoad()
        {
            if (HasConnector("Width")) ctl.Width = this["Width"].GetValue<int>();
            if (HasConnector("Height")) ctl.Height = this["Height"].GetValue<int>();

            base.OnAfterLoad();
        }

        public override object GetUIElement()
        {
            //WinUI.FlowLayoutPanel pnl = new WinUI.FlowLayoutPanel();
            ctl.Controls.Clear();

            foreach (string id in innerWeb.Blocks)
            {
                object item = innerWeb[id].ProcessRequest("GetUIElement");

                if (item != null)
                {
                    ctl.Controls.Add(item as WinUI.Control);
                }
            }
            //pnl.BorderStyle = WinUI.BorderStyle.FixedSingle;
            //pnl.Dock = WinUI.DockStyle.Fill;

            //ctl.Controls.Add(pnl);

            //if (HasConnector("icon"))
            //{
            //    Stream s = this["icon"].GetValue<Stream>();

            //    //ctl.Icon = Icon.FromHandle(IntPtr.Zero);
            //}

            return ctl;
        }
    }
}
