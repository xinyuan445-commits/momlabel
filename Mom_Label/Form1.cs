using System;
using System.Windows.Forms;
using Mom_Label.Pages;

namespace Mom_Label
{
    public partial class Form1 : Form
    {
        // 声明页面逻辑控制类
        private ProductionOrderPage _productionOrderPage;
        private ProcessCardPage _processCardPage; // 声明工序流转单控制类

        public Form1()
        {
            InitializeComponent();

            // 窗体加载时初始化各个页签的逻辑
            this.Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            // 初始化“生产订单”页签逻辑
            _productionOrderPage = new ProductionOrderPage(textBox1, button1, button2, dataGridView1);
            
            // 初始化“工序流转单”页签逻辑
            // 绑定 tabPage5 上的控件 (textBox2, button3[查询], button4[预览], dataGridView2)
            _processCardPage = new ProcessCardPage(textBox2, button3, button4, dataGridView2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
