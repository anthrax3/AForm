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
using DBML.Common.Dynamic;
using DBML.Common.Criteria;

namespace AForm.Win.Controls
{
    [BlockHandle("DropDown")]
    public class DropDown: WinControlBase<WinUI.ComboBox>
    {
        private DynamicRowCollection myRows = null;
        private BlockProperty<object> currentValue = null;
        private string valueFieldName = "";

        public DropDown(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitConnectors()
        {
            currentValue = new BlockProperty<object>(this, "CurrentValue");
            base.InitConnectors();
        }

        [BlockService]
        public void initDDL()
        {
            //template.Bindings["Value"];
            //template.Bindings["Key"];

            string tableName = this["Table"].GetValue<string>();
            string displayFieldName = this["DisplayField"].GetValue<string>();
            valueFieldName = this["ValueField"].GetValue<string>();

            SelectCriteria sc = new SelectCriteria();
            sc.fields = new System.Collections.ArrayList();
            sc.fields.Add(displayFieldName);
            sc.fields.Add(valueFieldName);

            myRows = DynamicRow.FindRows(tableName, sc);

            foreach (DynamicRow row in myRows)
            {
                ctl.Items.Add(row[displayFieldName].AsObject());
            }

            ctl.SelectedIndexChanged += new EventHandler(ctl_SelectedIndexChanged);
        }

        void ctl_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentValue.SetValue(myRows[ctl.SelectedIndex][valueFieldName].AsObject());
        }
    }
}
