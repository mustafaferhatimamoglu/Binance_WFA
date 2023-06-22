using Binance.Spot;
using Binance.Spot.Models;
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
    public partial class Form3 : Form
    {
        public readonly BackgroundWorker bgW_BTCUSDT = new();
        long Time_2022 = 1640995200000;
        public Form3()
        {
            InitializeComponent();
            bgW_BTCUSDT.DoWork += new System.ComponentModel.DoWorkEventHandler(bgW_BTCUSDT_DoWorkAsync);
            bgW_BTCUSDT.RunWorkerAsync();
        }

        private async void bgW_BTCUSDT_DoWorkAsync(object? sender, DoWorkEventArgs e)
        {
            Market market = new Market();
            var asd4 = await market.KlineCandlestickData(
                    "BTCUSDT",
                    Interval.ONE_MINUTE,
                    Time_2022,
                    null,
                    1000);
        }

        void ParseKlineData(string gelen)
        {

        }
    }
}
