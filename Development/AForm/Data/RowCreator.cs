using System;
using System.Collections.Generic;
using System.Text;
using DCRF.Common.Core;
using DCRF.Common.Interface;
using DCRF.Common.Attributes;
using DCRF.Common.Contract.Impl;
using DBML.Common.Dynamic;

namespace AForm.Data
{
    public class RowCreator: BlockBase
    {
        private BlockProperty<string> tableName = null;

        public RowCreator(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void InitConnectors()
        {
            tableName = new BlockProperty<string>(this, "TableName");

            base.InitConnectors();
        }


        [BlockService]
        public void CreateRow()
        {
            string tbl = tableName.GetValue();

            DynamicRow dr = DynamicRow.CreateRow(tbl);

            foreach (string field in dr.FieldNames)
            {
                object value = blockWeb["frmMain"][field].ProcessRequest()[0];
                dr.SetFieldValue(field, value);
            }

            dr.SaveData();
        }

        [BlockService]
        public object GetUIElement()
        {
            return null;
        }
    }
}
