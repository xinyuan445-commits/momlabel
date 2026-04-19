using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Mom_Label.Utils;
using gregn6Lib;

namespace Mom_Label.Pages
{
    public class ProcessCardPage
    {
        private TextBox _txtMoCode;
        private Button _btnQuery;
        private Button _btnPreview;
        private DataGridView _dgvResult;

        // 声明锐浪报表对象
        private GridppReport _report;
        private DataTable _currentPrintData; // 用于存储当前要打印的明细数据

        public ProcessCardPage(TextBox txtMoCode, Button btnQuery, Button btnPreview, DataGridView dgvResult)
        {
            _txtMoCode = txtMoCode;
            _btnQuery = btnQuery;
            _btnPreview = btnPreview;
            _dgvResult = dgvResult;

            // 初始化锐浪报表实例
            _report = new GridppReport();
            
            // 绑定锐浪报表的 FetchRecord 事件，用于推送明细数据
            _report.FetchRecord += Report_FetchRecord;

            // 绑定事件
            _btnQuery.Click += BtnQuery_Click;
            _btnPreview.Click += BtnPreview_Click;
            
            // 初始化 DataGridView 样式
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            _dgvResult.AllowUserToAddRows = false;
            _dgvResult.AllowUserToDeleteRows = false;
            _dgvResult.ReadOnly = true;
            _dgvResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void BtnQuery_Click(object? sender, EventArgs e)
        {
            string moCode = _txtMoCode.Text.Trim();
            if (string.IsNullOrEmpty(moCode))
            {
                MessageBox.Show("请输入生产订单号 (工单号) 进行查询。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = @"
SELECT 
    a.MoCode_a AS '工单号', 
    a.InvCode_b AS '物料编码', 
    inv.cInvStd AS '规格型号', 
    inv.cInvDefine1 AS 'CKB', 
    a.Qty_b AS '数量' 
FROM momlist_yx a 
LEFT JOIN Inventory inv ON a.InvCode_b = inv.cInvCode 
WHERE a.mocode_a = @MoCode";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MoCode", SqlDbType.NVarChar) { Value = moCode }
                };

                DataTable dt = SqlHelper.ExecuteQuery(sql, parameters);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    _dgvResult.DataSource = dt;
                }
                else
                {
                    _dgvResult.DataSource = null;
                    MessageBox.Show("未查询到该工单号的数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPreview_Click(object? sender, EventArgs e)
        {
            if (_dgvResult.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先在表格中选中要预览打印的流转单行。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. 获取选中行的数据
                DataGridViewRow selectedRow = _dgvResult.SelectedRows[0];
                string moCode = selectedRow.Cells["工单号"].Value?.ToString() ?? "";
                string invCode = selectedRow.Cells["物料编码"].Value?.ToString() ?? "";
                string cInvStd = selectedRow.Cells["规格型号"].Value?.ToString() ?? "";
                string ckb = selectedRow.Cells["CKB"].Value?.ToString() ?? "";
                string qty = selectedRow.Cells["数量"].Value?.ToString() ?? "";

                // 2. 生成二维码 (传入 JSON 文本)
                string qrContent = $"{{\"mocode\": \"{moCode}\"}}";
                
                // 3. 加载锐浪报表模板
                string reportFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pages", "ProcessCardReport.grf");
                if (File.Exists(reportFile))
                {
                    _report.LoadFromFile(reportFile);
                }
                else
                {
                    MessageBox.Show($"找不到锐浪报表模板文件: {reportFile}", "提示");
                    return;
                }

                // 4. 将提取的数据通过 参数(Parameter) 的方式传给报表表头
                SetParameterValue("工单号", moCode);
                SetParameterValue("物料编码", invCode);
                SetParameterValue("规格型号", cInvStd);
                SetParameterValue("CKB", ckb);
                SetParameterValue("数量", qty);
                SetParameterValue("QRCodeText", qrContent);

                // 准备好要推送的明细数据
                _currentPrintData = _dgvResult.DataSource as DataTable;

                // 5. 弹出锐浪自带的打印预览界面
                _report.PrintPreview(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"预览生成失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 锐浪报表请求数据的回调事件
        /// 在这里把 DataTable 的每一行数据推送给报表
        /// </summary>
        private void Report_FetchRecord()
        {
            if (_currentPrintData == null || _currentPrintData.Rows.Count == 0) return;

            foreach (DataRow row in _currentPrintData.Rows)
            {
                _report.DetailGrid.Recordset.Append();

                foreach (DataColumn col in _currentPrintData.Columns)
                {
                    var field = _report.FieldByName(col.ColumnName);
                    if (field != null && row[col] != DBNull.Value)
                    {
                        field.Value = row[col];
                    }
                }

                _report.DetailGrid.Recordset.Post();
            }
        }

        /// <summary>
        /// 安全设置锐浪报表参数
        /// </summary>
        private void SetParameterValue(string paramName, string paramValue)
        {
            var param = _report.ParameterByName(paramName);
            if (param != null)
            {
                param.Value = paramValue;
            }
        }
    }
}