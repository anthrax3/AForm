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

namespace AForm.Win.Controls
{
    [BlockHandle("CheckBox")]
    public class CheckBox : WinControlBase<WinUI.CheckBox>
    {
        public CheckBox(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void OnAfterLoad()
        {
            ctl.Text = this["Text"].GetValue<string>("chk");
        }
    }
}
