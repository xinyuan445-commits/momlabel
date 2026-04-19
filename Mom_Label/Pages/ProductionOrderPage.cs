using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Mom_Label.Utils;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;
using gregn6Lib;

namespace Mom_Label.Pages
{
    public class ProductionOrderPage
    {
        private TextBox _txtMoCode;
        private Button _btnQuery;
        private Button _btnPreview;
        private DataGridView _dgvResult;

        // 声明锐浪报表对象
        private GridppReport _report;

        private DataTable _currentPrintData; // 用于存储当前要打印的明细数据

        public ProductionOrderPage(TextBox txtMoCode, Button btnQuery, Button btnPreview, DataGridView dgvResult)
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
    a.MoCode AS '工单号',
    dp.cDepName AS '生产部门',
    inv.cinvcode AS '料号',
    inv.cinvname AS '料名',
    inv.cInvStd AS '规格型号',
    b.Qty AS '数量',
    c.StartDate AS '开工',
    c.DueDate AS '完工',
    a.Createtime,
    bom.Version AS 'bom版本',
    b.OrderCode AS '需求跟踪号',
    b.OrderSeq AS '需求跟踪行号',
    u2.cUser_Name AS '审核人',
    u1.cUser_Name AS '制单人',
    ISNULL(b.RelsTime, '') AS '审核时间',
    b.SortSeq AS '行号',
    CASE 
        WHEN LEFT(b.invcode, 1) = '1' THEN '成品' 
        WHEN LEFT(b.invcode, 1) = '2' THEN '半成品' 
    END AS '产品类别',
    b.remark,
    dp.cDepMemo,
    b.MoLotCode   
FROM mom_order a 
LEFT JOIN mom_orderdetail b ON a.MoId = b.MoId  
LEFT JOIN mom_morder c ON b.ModId = c.MoDId 
LEFT JOIN Inventory inv ON inv.cInvCode = b.InvCode 
LEFT JOIN Department dp ON b.MDeptCode = dp.cDepCode  
LEFT JOIN (
    SELECT a.Version, c.InvCode, a.BomId 
    FROM bom_bom a 
    LEFT JOIN bom_parent b ON a.BomId = b.BomId 
    LEFT JOIN bas_part c ON b.ParentId = c.PartId 
    WHERE a.CloseUser IS NULL AND InvCode IS NOT NULL 
) bom ON bom.InvCode = b.InvCode AND b.bomid = bom.bomid  
LEFT JOIN UFSystem..UA_User u1 ON a.CreateUser = u1.cUser_Id 
LEFT JOIN UFSystem..UA_User u2 ON u2.cUser_Id = b.RelsUser 
WHERE a.Mocode = @MoCode";

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
                MessageBox.Show("请先在表格中选中要预览打印的工单行。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. 获取选中行的数据
                DataGridViewRow selectedRow = _dgvResult.SelectedRows[0];
                string moCode = selectedRow.Cells["工单号"].Value?.ToString() ?? "";
                string invCode = selectedRow.Cells["料号"].Value?.ToString() ?? "";
                string invName = selectedRow.Cells["料名"].Value?.ToString() ?? "";
                string cDepName = selectedRow.Cells["生产部门"].Value?.ToString() ?? "";
                
                // 处理时间格式，避免带上时分秒
                string startDate = "";
                if (selectedRow.Cells["开工"].Value != null && selectedRow.Cells["开工"].Value != DBNull.Value)
                {
                    startDate = Convert.ToDateTime(selectedRow.Cells["开工"].Value).ToString("yyyy-MM-dd");
                }

                string dueDate = "";
                if (selectedRow.Cells["完工"].Value != null && selectedRow.Cells["完工"].Value != DBNull.Value)
                {
                    dueDate = Convert.ToDateTime(selectedRow.Cells["完工"].Value).ToString("yyyy-MM-dd");
                }

                // 判断审核时间生成带状态的工单号标题
                string auditTime = selectedRow.Cells["审核时间"].Value?.ToString() ?? "";
                string statusText = string.IsNullOrEmpty(auditTime) ? "(未审核)" : "(已审核)";
                string formattedMoCode = $"生产订单 - {moCode} {statusText}";

                // 2. 生成二维码 (锐浪本身支持直接传文本生成二维码，但如果想保持自己生成的控制权，可以传图片或 Base64)
                string qrContent = $"工单号:{moCode}|料号:{invCode}";
                
                // 3. 加载锐浪报表模板
                // 因为文件现在放在 Pages 目录下，我们构建它的路径
                string reportFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pages", "ProductionReport.grf");
                if (File.Exists(reportFile))
                {
                    _report.LoadFromFile(reportFile);
                }
                else
                {
                    MessageBox.Show($"找不到锐浪报表模板文件: {reportFile}", "提示");
                    return;
                }

                // 4. 将提取的数据通过 参数(Parameter) 的方式传给报表
                // 在您的锐浪报表模板里，定义了中文名字的参数
                //MessageBox.Show(formattedMoCode);
                SetParameterValue("工单号", moCode);
                SetParameterValue("料号", invCode);
                SetParameterValue("料名", invName);
                SetParameterValue("开工", startDate);
                SetParameterValue("完工", dueDate);
                SetParameterValue("QRCodeText", qrContent);
                SetParameterValue("工单", formattedMoCode);
                SetParameterValue("部门", cDepName);

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

            // 报表会按照这里循环推送的次数生成明细行
            foreach (DataRow row in _currentPrintData.Rows)
            {
                // 通知报表开始追加新的一行
                _report.DetailGrid.Recordset.Append();

                // 遍历我们 DataTable 里的所有列，推送到报表对应的字段 (Field) 里
                foreach (DataColumn col in _currentPrintData.Columns)
                {
                    var field = _report.FieldByName(col.ColumnName);
                    if (field != null && row[col] != DBNull.Value)
                    {
                        // 把 DataTable 里单元格的值赋给报表对应的 Field
                        field.Value = row[col];
                    }
                }

                // 通知报表这行数据提交完毕
                _report.DetailGrid.Recordset.Post();
            }
        }

        /// <summary>
        /// 安全设置锐浪报表参数
        /// </summary>
        private void SetParameterValue(string paramName, string paramValue)
        {
            // 在 C# 调用锐浪 COM 时，通常使用 ParameterByName 或者参数集合的名称索引
            var param = _report.ParameterByName(paramName);
            if (param != null)
            {
                param.Value = paramValue;
            }
        }

        private byte[] GenerateQRCode(string content)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 200,
                    Width = 200,
                    Margin = 1
                }
            };

            using (var bitmap = writer.Write(content))
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}