using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Binance_WFA
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            // Load model and predict the next set values.
            // The number of values predicted is equal to the horizon specified while training.
            var result = MLModel1.Predict();


        }
    }
}
