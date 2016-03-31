using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using DCRF.Common.Core;
using DCRF.Common.Broker;
using DCRF.Common.Primitive;
using DCRF.Common.Definition;
using AForm.Win;
using AForm.Win.Forms;
using AForm.Win.Controls;
using DBML.Provider;
using DBML.Common.Provider;
using AForm.Data;
using AForm.Validator;
using DCRF.Common.Helper;
using DCRF.Common.XML;
using DCRF.Common.Interface;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;

namespace TestApp
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("state.txt"))
            {
                string[] data = File.ReadAllLines("state.txt");

                txtBlocksFolder.Text = data[0];
                txtFormFile.Text = data[1];
                txtBWID.Text = data[2];
            }

        }

        private void testAForm()
        {
            //RuntimeBlockBroker broker = new RuntimeBlockBroker();
            //BlockWeb bw = new BlockWeb("test", broker, false);

            //broker.AddBlock<WinForm>();
            //broker.AddBlock<Label>();
            //broker.AddBlock<TextBox>();
            //broker.AddBlock<Button>();
            //broker.AddBlock<SchemaBuilder>();
            //broker.AddBlock<FormBuilder>();
            //broker.AddBlock<RowFeeder>();
            //broker.AddBlock<DataSaver>();
            //broker.AddBlock<RequiredFieldValidator>();
            //broker.AddBlock<DataGrid>();
            //broker.AddBlock<GridFilterButton>();
            //broker.AddBlock<LookupLabel>();
            //broker.AddBlock<DropDown>();


            //string sb = bw.AddBlock(BlockHandle.New("SchemaBuilder"));
            //string fb = bw.AddBlock(BlockHandle.New("FormBuilder"));

            //DBML.DBCore.file = @"d:\My\MyDev\AForm\DataSource\db.s3db";
            //DBML.DBCore.initializeProvider(DBML.ProviderType.SQLite);

            ////FormSchema fs = (FormSchema) bw[sb].ProcessRequest("Build", DBML.DBCore.getInstance(), "TABLE1");
            //FormSchema fs = (FormSchema)bw[sb].ProcessRequest("LoadFromFile", "form.xml");


            //string formId = (string)bw[fb].ProcessRequest("BuildWinForm", fs);

            //bw[formId].ProcessRequest("Attach", panel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RuntimeBlockBroker broker = initBroker();

            IBlockWeb bw = XMLLoader.LoadBlockWeb("blockWeb-copy.xml", "a", broker, null);
            string formId = bw.GetConnector("FormId").GetValue<string>();
            System.Windows.Forms.Form frm = bw[formId].ProcessRequest("GetUIElement") as System.Windows.Forms.Form;
            frm.ShowDialog();
        }

        private static RuntimeBlockBroker initBroker()
        {
            DBML.DBCore.file = @"d:\My\MyDev\AForm\DataSource\db.s3db";
            DBML.DBCore.initializeProvider(DBML.ProviderType.SQLite);


            //testAForm();
            RuntimeBlockBroker broker = new RuntimeBlockBroker();

            broker.AddBlock<WinForm>();
            broker.AddBlock<Label>();
            broker.AddBlock<TextBox>();
            broker.AddBlock<Button>();
            broker.AddBlock<RowFeeder>();
            broker.AddBlock<DataSaver>();
            broker.AddBlock<RequiredFieldValidator>();
            broker.AddBlock<DataGrid>();
            broker.AddBlock<GridFilterButton>();
            broker.AddBlock<LookupLabel>();
            broker.AddBlock<DropDown>();
            broker.AddBlock<TableLayout>();
            broker.AddBlock<CheckBox>();
            return broker;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //RuntimeBlockBroker broker = initBroker();
            //NodeContext.Broker = broker;
            //NewParser.init();

            //IBlockWeb web = NewParser.LoadBlockWeb("blockWeb-copy.xml", "a");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SimpleBlockBroker broker = new SimpleBlockBroker();
            RepositoryOptions ro = new RepositoryOptions();
            ro.Folder = txtBlocksFolder.Text;

            broker.SetupBroker(ro);

            IBlockWeb bw = XMLLoader.LoadBlockWeb(txtFormFile.Text, txtBWID.Text, broker, null);
            string formId = bw.GetConnector("FormId").GetValue<string>();
            System.Windows.Forms.Form frm = bw[formId].ProcessRequest("GetUIElement") as System.Windows.Forms.Form;
            frm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtBlocksFolder.Text = fbd.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFormFile.Text = ofd.FileName;
            }
        }

        private void txtBlocksFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            string[] data = new string[3];

            data[0] = txtBlocksFolder.Text;
            data[1] = txtFormFile.Text;
            data[2] = txtBWID.Text;

            File.WriteAllLines("state.txt", data);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmGen frm = new frmGen();
            frm.blocksFolder = txtBlocksFolder.Text;

            frm.ShowDialog();
        }
    }
}
