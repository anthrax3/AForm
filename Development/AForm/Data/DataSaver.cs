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
using DCRF.Common.Primitive;

namespace AForm.Data
{
    [BlockHandle("DataSaver")]
    public class DataSaver : BlockBase
    {
        private IDataRow row = null;

        public DataSaver(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        [BlockService]
        public void SaveData()
        {
            if (row == null)
            {
                row = DynamicRow.FindRow("TABLE1", 3);
            }

            foreach (string name in row.FieldNames)
            {
                if (name != "PK_ID")
                {
                    Type fieldType = row.GetFieldType(name);
                    object value = blockWeb["frmMain"][name].ProcessRequest()[0];
                    object newValue = Convert.ChangeType(value, fieldType);
                    
                    row.SetFieldValue(name, newValue);
                }
            }

            (row as DynamicRow).SaveData();
        }

        [BlockService]
        public void DeleteData(string tbl, int pkid)
        {
            DynamicRow.DeleteRow<int>(tbl, "PK_ID", pkid);
        }

        [BlockService]
        public object GetUIElement()
        {
            return null;
        }
    }
}
