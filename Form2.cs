using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binance.Spot;
using Binance;
using System.Diagnostics;
using Binance.Spot.Models;

namespace Binance_WFA
{
    public partial class Form2 : Form
    {
        public readonly BackgroundWorker bgW_CheckServerTime = new();
        public Form2()
        {
            InitializeComponent();
            bgW_CheckServerTime.DoWork += new System.ComponentModel.DoWorkEventHandler(bgW_CheckServerTime_DoWork);
            bgW_CheckServerTime.RunWorkerAsync();
        }

        string serverTime;
        private async void bgW_CheckServerTime_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Market market = new Market();
                serverTime = await market.CheckServerTime();
                //var price = await market.GetPriceAsync("BTCUSDT");
                var asd = await market.CurrentAveragePrice("BTCUSDT");
                var asd2 = await market.RollingWindowPriceChangeStatistics("BTCUSDT");
                var asd3 = await market.KlineCandlestickData(
                    "BTCUSDT",
                    Interval.ONE_MINUTE,
                    (long)System.Convert.ToDouble(serverTime.Split(":")[1].Split("}")[0]) - 1000 * 60 * 60 * 24,
                    (long)System.Convert.ToDouble(serverTime.Split(":")[1].Split("}")[0]),
                    100000);
                var asd4 = await market.KlineCandlestickData(
                    "BTCUSDT",
                    Interval.ONE_MINUTE,
                    (long)System.Convert.ToDouble(serverTime.Split(":")[1].Split("}")[0]) - 1000 * 60 * 60 * 24,
                    null,
                    1000);
                asd3 = asd3.Replace("],[", "\n");
                asd4 = asd4.Replace("],[", "\n");

                DateTime serverDateTime = DateTimeOffset.FromUnixTimeMilliseconds((long)System.Convert.ToDouble(serverTime.Split(":")[1].Split("}")[0])).DateTime;
                DateTime localDateTime = serverDateTime.ToLocalTime();
                Console.WriteLine(localDateTime);
                Console.WriteLine(serverTime);
                Invoke(new Action(delegate () { Update_UI_Work(); }));
                //richTextBox1.Text = serverTime;
            }
        }
        void Update_UI_Work()
        {
            richTextBox1.Text = serverTime + "\n" + DateTime.Now.Ticks;
        }
    }
}
