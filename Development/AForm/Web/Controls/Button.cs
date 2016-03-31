using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Attributes;
using WebUI = System.Web.UI;
using DCRF.Common.Core;
using DCRF.Common.Interface;
using DCRF.Common.Definition;
using DCRF.Common.Primitive;
using DCRF.Common.Contract.Impl;
using AForm.Web.Base;

namespace AForm.Web.Controls
{
    [BlockHandle("Button")]
    public class Button : WebControlBase<WebUI.WebControls.Button>
    {
        BlockEvent clickEvent = null;

        public Button(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitBlock()
        {
            base.InitBlock();

            clickEvent = new BlockEvent(this, "Click");
        }

        public override void OnAfterLoad()
        {
            ctl.Click +=
              new EventHandler(
                  delegate(object sender, EventArgs e)
                  {
                      clickEvent.Raise();
                  }
              );
        }
    }
}
