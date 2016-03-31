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

namespace AForm.Win.Controls
{
    [BlockHandle("Label")]
    [BlockTag("AControl")]
    public class Label : WinControlBase<WinUI.Label>
    {
        public Label(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void OnAfterLoad()
        {
            ctl.AutoSize = this["AutoSize"].GetValue<bool>(true);

            base.OnAfterLoad();
        }
    }
}
