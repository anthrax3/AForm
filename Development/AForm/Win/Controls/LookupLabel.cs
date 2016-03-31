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
using DBML.Common.Dynamic;

namespace AForm.Win.Controls
{
    /// <summary>
    /// Bound to a field, text of label is in "TABLEx.FIELDy" whose PK_ID is bound value, 
    /// </summary>
    [BlockHandle("LookupLabel")]
    public class LookupLabel: WinControlBase<WinUI.Label>
    {
        public LookupLabel(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void OnAfterLoad()
        {
            ctl.BorderStyle = WinUI.BorderStyle.FixedSingle;
            ctl.Width = 500;
            ctl.Text = this["Text"].GetValue<string>();
            //string srcId = ParentFormId;

            string binding = this["Key"].GetValue<string>();

            blockWeb["frmMain"][SysEventCode.EndPointAttached, binding].AttachEndPoint(Id, "SetKey");

            base.OnAfterLoad();
        }

        //public override Dictionary<string, string> InitFromSchema(ControlSchema template, string controlName, string formId)
        //{
        //    ctl.BorderStyle = WinUI.BorderStyle.FixedSingle;
        //    ctl.Width = 500;

        //    return base.InitFromSchema(template, controlName, formId);
        //}

        [BlockService]
        public void SetKey(ConnectorSysEventArgs eventArgs)
        {
            if (eventArgs.EndPointValue != null)
            {
                string table = this["Table"].GetValue<string>();
                string displayField = this["DisplayField"].GetValue<string>();
                string pkField = this["PKField"].GetValue<string>();

                //seelct displayField from targetTable where PK_ID = eventArgs.EndPointValue
                DynamicRow myRow = DynamicRow.FindRow<long>(table, (long)eventArgs.EndPointValue);
                ctl.Text = "pk=" + (eventArgs.EndPointValue.ToString()) + " value=" + myRow[displayField].fValue.ToString();

            }
        }
    }
}
