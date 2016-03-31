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

namespace AForm.Win.Controls
{
    [BlockHandle("TextBox")]
    [BlockTag("AControl")]
    public class TextBox : WinControlBase<WinUI.TextBox>
    {
        private BlockEvent textChanged = null;

        public TextBox(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitConnectors()
        {
            base.InitConnectors();

            textChanged = new BlockEvent(this, "TextChanged");
        }

        [BlockService]
        public string GetValue()
        {
            return ctl.Text;
        }

        [BlockService]
        public string GetText()
        {
            return ctl.Text;
        }

        [BlockService]
        public void SetText(ConnectorSysEventArgs args)
        {
            if (args.EndPointValue != null)
            {
                ctl.Text = args.EndPointValue.ToString();
            }

            if (args.RequestArgs != null && args.RequestArgs[0] is ConnectorSysEventArgs)
            {
                ConnectorSysEventArgs args2 = args.RequestArgs[0] as ConnectorSysEventArgs;

                if (args2.EndPointValue != null)
                {
                    ctl.Text = args2.EndPointValue.ToString();
                }
            }

        }

        [BlockService]
        public void Msg()
        {
            System.Windows.Forms.MessageBox.Show("Hello!");
        }

        [BlockService]
        public int Add(int x, int y) { return x + y; }

    }
}
