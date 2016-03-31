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

namespace AForm.Win.Controls
{
    [BlockHandle("DataGrid")]
    public class DataGrid: WinControlBase<WinUI.DataGridView>
    {
        public DataGrid(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void OnAfterLoad()
        {
            ctl.Width = 400;
            ctl.Height = 400;

            //ParentFormId = formId;

            //in order to create a DataGridView we need two things:
            //1 - information about columns (title, databinding, ...)
            //2 - actual data rows

            //TODO: define an object which contains all related column info and read it from properties
            RefreshGrid(null);
        }

        [BlockService]
        public void RefreshGrid(BaseCriteriaCollection bcc) //ControlSchema template, BaseCriteriaCollection criteria=null)
        {
            string tableName = this["TableName"].GetValue<string>("");
            string[] columns = this["Columns"].GetValue<string[]>(null);
            string[] titles = this["ColumnHeaders"].GetValue<string[]>(null);

            int i = 0;
            DataTable dt = new DataTable();
            SelectCriteria sc = new SelectCriteria();
            sc.fields = new System.Collections.ArrayList();

            ctl.Columns.Clear();

            foreach (string column in columns)
            {
                WinUI.DataGridViewTextBoxColumn col = new WinUI.DataGridViewTextBoxColumn();
                col.HeaderText = titles[i++];
                col.DataPropertyName = column;
                col.Name = column;

                ctl.Columns.Add(col);

                dt.Columns.Add(column);
                sc.fields.Add(column);
            }

            if (bcc != null)
            {
                sc.Criteria = bcc;
            }

            //sc.Criteria = criteria;

            DynamicRowCollection rows = DynamicRow.FindRows(tableName, sc);

            for (i = 0; i < rows.Count; i++)
            {
                DataRow dr = dt.NewRow();

                foreach (string column in columns) dr[column] = rows[i][column].fValue;

                dt.Rows.Add(dr);
            }

            ctl.DataSource = dt;
        }
    }
}
