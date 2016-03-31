using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DBML.Interface.Provider;
using DCRF.Common.Broker;
using DCRF.Common.Primitive;

namespace TestApp
{
    public partial class frmGen : Form
    {
        public string blocksFolder = null;


        public frmGen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFile.Text = ofd.FileName;
            }
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            cboTable.Items.Clear();

            if (File.Exists(txtFile.Text))
            {
                DBML.DBCore.file = txtFile.Text;
                DBML.DBCore.initializeProvider(DBML.ProviderType.SQLite);

                DBTable[] tables = DBML.DBCore.getInstance().GetTablesInfo();

                foreach (DBTable tbl in tables)
                {
                    cboTable.Items.Add(tbl.tableName);
                }

                cboTable.Enabled = true;
                cboTable.SelectedIndex = -1;
            }
            else
            {
                cboTable.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cboTable.SelectedIndex == -1)
            {
                return;
            }

        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTable.SelectedIndex == -1)
            {
                lstFields.Items.Clear();
                return;
            }

            string tblName = cboTable.SelectedItem.ToString();
            DBField[] fields = DBML.DBCore.getInstance().GetFieldsInfo(tblName);

            foreach (DBField field in fields)
            {
                addFieldToList(field);
            }

            lstFields.SelectedIndices.Clear();
        }

        private void addFieldToList(DBField field)
        {
            ListViewItem lvi = new ListViewItem();

            FieldInfo fi = new FieldInfo();
            fi.dbField = field;

            lvi.Tag = fi;
            lvi.Text = field.name;
            lvi.SubItems.Add(field.dataType.ToString());
            lvi.SubItems.Add("?");

            lstFields.Items.Add(lvi);
        }

        private void frmGen_Load(object sender, EventArgs e)
        {
            SimpleBlockBroker broker = new SimpleBlockBroker();
            RepositoryOptions ro = new RepositoryOptions();
            ro.Folder = blocksFolder;

            broker.SetupBroker(ro);

            foreach (BlockHandle bh in broker.Blocks)
            {
                cboClass.Items.Add(bh.ClassName);
            }
        }

        private void lstFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFields.SelectedIndices.Count == 0)
            {                
                cboClass.Enabled = false;
                return;
            }

            cboClass.Enabled = true;

            ListViewItem selectedField = lstFields.SelectedItems[0];
            FieldInfo fi = selectedField.Tag as FieldInfo;

            if (fi.ctlClassName != null)
            {
                cboClass.SelectedItem = fi.ctlClassName;
            }
            else
            {
                cboClass.SelectedIndex = -1;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ListViewItem selectedField = lstFields.SelectedItems[0];

            FieldInfo sinfo = selectedField.Tag as FieldInfo;

            if (cboClass.SelectedIndex == -1)
            {
                sinfo.ctlClassName = null;
            }
            else
            {
                sinfo.ctlClassName = cboClass.SelectedItem.ToString();
            }

            selectedField.SubItems[2].Text = sinfo.ctlClassName;
        }
    }
}
