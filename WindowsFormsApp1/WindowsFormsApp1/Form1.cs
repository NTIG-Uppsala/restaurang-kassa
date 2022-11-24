using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int position = 100;
        public int price = 0;
        public int count = 0;

        public void button2_Click(object sender, EventArgs e)
        {
            var lblnew = new Label
            {
                Location = new Point(500, position),
                Text = button2.Text, //Text can be dynamically assigned e.g From some text box
                Name = "createdLabel",
                AutoSize = true,
                BackColor = Color.LightGray,
                Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, (byte)0),
            };
            //this refers to current form you can use your container according to requirement
            // Controls.Add(lblnew);
            position = position + 30;
            price = price + 10;
            count += 1;
            label1.Text = price.ToString() + "kr";
            listBox1.Items.Add(button2.Text);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            var lblnew = new Label
            {
                Location = new Point(500, position),
                Text = button1.Text, //Text can be dynamically assigned e.g From some text box
                Name = "createdLabel",
                AutoSize = true,
                BackColor = Color.LightGray,
                Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, (byte)0)
            };
            //this refers to current form you can use your container according to requirement
            // Controls.Add(lblnew);
            position = position + 30;
            price = price + 10;
            count += 1;
            label1.Text = price.ToString() + "kr";
            listBox1.Items.Add(button1.Text);
        }

        public void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < count; i++)
            { 
                var labelToRemove = this.Controls["createdLabel"];
                this.Controls.Remove(labelToRemove);
            }
            count = 0;
            price = 0;
            position = 100;
            label1.Text = price.ToString() + "kr";
            listBox1.Items.Clear();
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
