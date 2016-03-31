using System;
using System.Collections.Generic;
using System.Text;
using AForm.Schema;

namespace AForm.Base
{
    public interface IControlBase
    {
        Dictionary<string, Guid> InitFromTemplate(ControlSchema template, Guid formId);
        void InitControl();
        void Attach(object container);
    }
}
