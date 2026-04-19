using System;
using System.Windows.Forms;
using Mom_Label.Pages;

namespace Mom_Label
{
    public partial class Form1 : Form
    {
        // 声明页面逻辑控制类
        private ProductionOrderPage _productionOrderPage;

        public Form1()
        {
            InitializeComponent();
            
            // 窗体加载时初始化各个页签的逻辑
            this.Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            // 初始化“生产订单”页签逻辑
            // 将窗体上的控件 (textBox1, button1, button2, dataGridView1) 传递给专门的类进行管理
            _productionOrderPage = new ProductionOrderPage(textBox1, button1, button2, dataGridView1);
        }
    }
}
