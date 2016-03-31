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
    public class WinFormBase<T> : WinControlBase<T> where T : WinUI.Form, new()
    {
        private BlockEvent formSubmit = null;

        public WinFormBase(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitConnectors()
        {
            base.InitConnectors();

            formSubmit = new BlockEvent(this, "FormSubmit");
        }

        public override void InitBlock()
        {
            base.InitBlock();

            ctl.Dock = WinUI.DockStyle.Fill; 
        }

    }
}
