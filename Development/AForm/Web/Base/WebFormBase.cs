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
    public class WebFormBase<T> : WebControlBase<T> where T : WebUI.Control, new()
    {
        public WebFormBase(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }
    }
}
