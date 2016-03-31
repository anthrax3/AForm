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
using AForm.Base;
using AForm.Schema;

namespace AForm.Grid
{
    [BlockId("GridControl")]
    public class GridControl: BlockBase, IControlBase
    {
        private Guid ParentFormId = Guid.Empty;

        public GridControl(Guid id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        [BlockService]
        public Dictionary<string, Guid> InitFromTemplate(ControlSchema template, Guid formId)
        {
            ParentFormId = formId;

            //template will have three subitems in 'Controls'
            //first is header
            //last is footer
            //second (middle one) is body which will be repeated to the number of rows
            //for each repeat, we will update bindings and add a rowIndex property
            //bindings of rows will point to grid as bindingsource and also bindingKey will be as specified in the schema
            //BindingId will be PK of that row
            //In case of bindingsource=parentForm we will not set any information because form is data provider.
            return new Dictionary<string,Guid>();
        }

        [BlockService]
        public void InitControl()
        {
        }

        [BlockService]
        public void Attach(object container)
        {
        }
    }
}
