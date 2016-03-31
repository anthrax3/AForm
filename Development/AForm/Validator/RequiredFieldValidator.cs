using WinUI = System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Attributes;
using DCRF.Common.Core;
using DCRF.Common.Contract.Impl;
using DBML.Common.Provider;
using DBML.Common.Dynamic;
using DBML.Interface;
using DCRF.Common.Interface;
using DCRF.Common.Primitive;
using DCRF.Common.Definition;

namespace AForm.Validator
{
    [BlockHandle("RequiredFieldValidator")]
    public class RequiredFieldValidator: BlockBase
    {
        public RequiredFieldValidator(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitBlock()
        {
            base.InitBlock();

            innerWeb = new BlockWeb("temp", blockWeb.Broker, PlatformType.Neutral, this, false);
        }

        [BlockService]
        public void PerformValidation(ConnectorSysEventArgs eventArgs)
        {
            //string currentValue
            string currentValue = (string) blockWeb["frmMain"]["PK_ID"].ProcessRequest()[0];

            if (currentValue == null || currentValue.Length == 0)
            {
                eventArgs.CancelOperation = true;
                eventArgs.FastCancelOperation = true;

                //show inner controls
                foreach (string id in innerWeb.Blocks)
                {
                    blockWeb[id].ProcessRequest("SetVisible", true);
                }
            }
        }

        [BlockService]
        public void InitControl()
        {
            //blockWeb[ParentFormId]["FormSubmit"].AttachEndPoint(Id, "Validate");

            ////OR in case of fast validation
            //string textBox1Id = (string)blockWeb[ParentFormId].ProcessRequest("FindControl", "TextBox1");
            //blockWeb[textBox1Id]["LostFocus"].AttachEndPoint(Id, "Validate");
        }

        [BlockService]
        public bool Validate()
        {
            return true;
        }

        [BlockService]
        public virtual object GetUIElement()
        {
            WinUI.Panel pnl = new WinUI.Panel();

            foreach (string id in innerWeb.Blocks)
            {
                object item = innerWeb[id].ProcessRequest("GetUIElement");

                pnl.Controls.Add(item as WinUI.Control);
            }

            return pnl;
        }
    }
}
