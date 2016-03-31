using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Attributes;
using WebUI = System.Web.UI;
using DCRF.Common.Core;
using DCRF.Common.Interface;
using DCRF.Common.Definition;
using DCRF.Common.Primitive;

namespace AForm.Web.Base
{
    public class WebControlBase<T> : BlockBase where T : WebUI.Control, new()
    {
        protected T ctl = null;

        public WebControlBase(string id, IContainerBlockWeb parent)
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
    }
}
