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

namespace AForm.Data
{
    [BlockHandle("RowFeeder")]
    public class RowFeeder: BlockBase
    {
        private DynamicRow row = null;

        public RowFeeder(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void OnAfterLoad()
        {
            FeedRow(2);
        }

        [BlockService]
        public void FeedRow(int id)
        {
            row = DynamicRow.FindRow<int>("TABLE1", id);
            string form = this["Form"].GetValue<string>("");

            foreach (string name in row.FieldNames)
            {
                blockWeb[form][name].AttachEndPoint(row.GetFieldValue(name));
            }
        }

        [BlockService]
        public void FeedRowStr(string id)
        {
            FeedRow(int.Parse(id));
        }

        //[BlockService]
        //public Dictionary<string, string> InitFromSchema(ControlSchema template, string controlName, string formId)
        //{
        //    ParentFormId = formId;

        //    return new Dictionary<string, string>();
        //}

        //[BlockService]
        //public void Attach(object container)
        //{
        //}

        [BlockService]
        public object GetUIElement()
        {
            return null;
        }
    } 
}
