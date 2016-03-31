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
using System.Data;
using DBML.Common.Dynamic;
using DBML.Common.Criteria;
using DBML.Provider;
using DBML;
using DBML.Interface.Provider;

namespace AForm.Win.Controls
{
    [BlockHandle("GridFilterButton")]
    public class GridFilterButton : Button
    {
        public GridFilterButton(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void OnAfterLoad()
        {
            this["Click"].AttachEndPoint(Id, "ApplyFilter");

            base.OnAfterLoad();
        }

        [BlockService]
        public void ApplyFilter()
        {
            //where is my target grid?
            string gridName = this["GridName"].GetValue<string>();

            //look into properties to find controls for each field
            BaseCriteriaCollection bcc = new BaseCriteriaCollection();
            string[] ctls = this["FilterValueControls"].GetValue<string[]>();
            string[] fields = this["FilterFields"].GetValue<string[]>();
            string table = this["TableName"].GetValue<string>();

            IProvider provider = DBCore.getInstance();

            for(int i=0;i<ctls.Length;i++)
            {
                string ctl = ctls[i];
                string field = fields[i];

                string value = (string)blockWeb[ctl].ProcessRequest("GetValue");

                DBField fieldInfo = provider.GetField(table, field);
                bcc.AddCriteria(field, new DynamicCriteria(field, fieldInfo.dataType, value, "="));                
            }            

            blockWeb[gridName].ProcessRequest("RefreshGrid", bcc);

            WinUI.MessageBox.Show("Filter applied");
        }
    }
}
