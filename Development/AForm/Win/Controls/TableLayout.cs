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

namespace AForm.Win.Controls
{
    [BlockHandle("TableLayout")]
    public class TableLayout: WinControlBase<WinUI.TableLayoutPanel>
    {
        public TableLayout(string id, IContainerBlockWeb parent)
            : base(id, parent)
        {
        }

        public override void OnAfterLoad()
        {
            int rows = this["Rows"].GetValue<int>(2);
            int cols = this["Columns"].GetValue<int>(2);
            string[] colStyles = this["ColumnStyles"].GetValue<string[]>();

            for (int i = 0; i < rows; i++)
            {
                ctl.RowStyles.Add(new WinUI.RowStyle(WinUI.SizeType.Percent,10f));
                ctl.RowCount++;
            }

            for (int i = 0; i < cols; i++)
            {
                if (colStyles[i].EndsWith("%"))
                {
                    int width = int.Parse(colStyles[i].Replace("%",""));
                    ctl.ColumnStyles.Add(new WinUI.ColumnStyle(WinUI.SizeType.Percent, width));
                }
                else
                {
                    int width = int.Parse(colStyles[i]);
                    ctl.ColumnStyles.Add(new WinUI.ColumnStyle(WinUI.SizeType.Absolute, width));
                }

                ctl.ColumnCount++;
            }
            
            ctl.CellBorderStyle = WinUI.TableLayoutPanelCellBorderStyle.Single;
            
            base.OnAfterLoad();
        }

        public override object GetUIElement()
        {
            foreach (string id in innerWeb.Blocks)
            {
                if (innerWeb[id].HasConnector("Row"))
                {
                    int r = innerWeb[id]["Row"].GetValue<int>(1);
                    int c = innerWeb[id]["Column"].GetValue<int>(1);

                    WinUI.Control child = innerWeb[id].ProcessRequest("GetUIElement") as WinUI.Control;

                    ctl.Controls.Add(child, c, r);
                }
            }

            return base.GetUIElement();
        }
    }
}
