using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Contract.Impl;
using DCRF.Common.Attributes;
using DCRF.Common.Interface;
using WinUI = System.Windows.Forms;
using DCRF.Common.Core;
using DCRF.Common.Definition;


namespace AForm.Win.Base
{
    public abstract class WinControlBase<T> : BlockBase where T : WinUI.Control, new()
    {
        protected T ctl = null;

        public WinControlBase(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitBlock()
        {
            base.InitBlock();
            ctl = new T();
        }

        [BlockService]
        public virtual object GetUIElement()
        {
            return ctl;
        }

        public override void OnAfterLoad()
        {
            if (HasConnector("Dock"))
            {
                string dock = this["Dock"].GetValue<string>("");

                if (dock == "Fill") ctl.Dock = WinUI.DockStyle.Fill;
            }

            if (HasConnector("Anchor"))
            {
                string anchor = this["Anchor"].GetValue<string>("");

                if (anchor == "Right") ctl.Anchor = WinUI.AnchorStyles.Right;
                if (anchor == "Left") ctl.Anchor = WinUI.AnchorStyles.Left;
                if (anchor == "None") ctl.Anchor = WinUI.AnchorStyles.None;
            }
            
            ctl.Text = this["Text"].GetValue<string>("");

            if (HasConnector("Visible"))
            {
                ctl.Visible = this["Visible"].GetValue<bool>(true);
            }

            if (HasConnector("Bold"))
            {
                bool isBold = this["Bold"].GetValue<bool>();

                if (isBold)
                {
                    ctl.Font = new System.Drawing.Font(ctl.Font, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    ctl.Font = new System.Drawing.Font(ctl.Font, System.Drawing.FontStyle.Regular);
                }
            }

            base.OnAfterLoad();
        }
    }
}
