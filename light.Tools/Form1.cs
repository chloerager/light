using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace light.Tools
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void button1_Click(object sender, EventArgs e)
      {
          //GETGB2312HZ hz = new GETGB2312HZ();
          //hz.Parse();
         ImportMySQL.Import();
      }
   }
}
