using Binance.Spot;
using Binance;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Text;
using System.Globalization;
using System.Web;
using Binance.Common;

using Newtonsoft.Json;
using System.Reflection;

namespace Binance_WFA
{
    public partial class Form1 : Form
    {
        public readonly BackgroundWorker bgW_CheckServerTime = new();
        string[] ?asd;

        string? serverTime;
        string? old_serverTime;
        List<string> times = new List<string>();


        private IBinanceSignatureService signatureService;
        private string baseUrl;
        private HttpClient httpClient;
        public Form1()
        {
            InitializeComponent();
            bgW_CheckServerTime.DoWork += new System.ComponentModel.DoWorkEventHandler(bgW_CheckServerTime_DoWork);
            bgW_CheckServerTime.RunWorkerAsync();
            //var client = new Binance

        }
        private async void bgW_CheckServerTime_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int i = 0;
                Market market = new Market();
                serverTime = await market.CheckServerTime();
                if (old_serverTime == serverTime)
                {
                    i++;
                }
                else
                {
                    old_serverTime = serverTime;
                    i = 0;
                }
                //button1.Text = i + serverTime;
                times.Add(i + "_" + serverTime+"_"+ DateTime.Now.ToString("yyyyMMddHHmmssff"));

                // HMAC signature
                var aaa = new SpotAccountTrade(httpClient, new BinanceHmac(secretKey), apiKey: apiKey);


            }
        }        
    }
}