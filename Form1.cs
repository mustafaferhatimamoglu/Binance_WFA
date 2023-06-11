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


                //var asd2 = new SpotAccountTrade(httpClient, new BinanceRsa(privateKey), apiKey: "apiKey");
                // HMAC signature
                var aaa = new SpotAccountTrade(httpClient, new BinanceHmac(secretKey), apiKey: apiKey);


                ////var binance = new Binance.Spot();
                //var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac("apiSecret"));

                //websocket.OnMessageReceived(
                //    async (data) =>
                //    {
                //        Console.WriteLine(data);
                //        await Task.CompletedTask;
                //    }, CancellationToken.None);

                //await websocket.ConnectAsync(CancellationToken.None);
                //await websocket.AccountTrade.NewOrderAsync(symbol: "BNBUSDT", side: Models.Side.BUY, type: Models.OrderType.LIMIT, timeInForce: Models.TimeInForce.GTC, price: 300, quantity: 1, cancellationToken: CancellationToken.None);

                //await Task.Delay(5000);
                //Console.WriteLine("Disconnect with WebSocket API Server");
                //await websocket.DisconnectAsync(CancellationToken.None);




                //var binanceClient = new Binance.Client();
                //// Getting info on all symbols
                //var spotSymbolData = await binanceClient.SpotApi.ExchangeData.GetExchangeInfoAsync();

                //// Getting ticker
                //var spotTickerData = await binanceClient.SpotApi.ExchangeData.GetTickersAsync();

                //// Getting the order book of a symbol
                //var spotOrderBookData = await binanceClient.SpotApi.ExchangeData.GetOrderBookAsync("BTCUSDT");

                //// Getting recent trades of a symbol
                //var spotTradeHistoryData = await binanceClient.SpotApi.ExchangeData.GetTradeHistoryAsync("BTCUSDT");
            }
        }
        #region boþ
        //private const string RECENT_TRADES_LIST = "/api/v3/trades";

        ///// <summary>
        ///// Get recent trades.<para />
        ///// Weight(IP): 1.
        ///// </summary>
        ///// <param name="symbol">Trading symbol, e.g. BNBUSDT.</param>
        ///// <param name="limit">Default 500; max 1000.</param>
        ///// <returns>Trade list.</returns>
        //public async Task<string> RecentTradesList(string symbol, int? limit = null)
        //{
        //    var result = await this.SendPublicAsync<string>(
        //        RECENT_TRADES_LIST,
        //        HttpMethod.Get,
        //        query: new Dictionary<string, object>
        //        {
        //            { "symbol", symbol },
        //            { "limit", limit },
        //        });

        //    return result;
        //}
        //protected async Task<T> SendPublicAsync<T>(string requestUri, HttpMethod httpMethod, Dictionary<string, object> query = null, object content = null)
        //{
        //    if (!(query is null))
        //    {
        //        StringBuilder queryStringBuilder = this.BuildQueryString(query, new StringBuilder());

        //        if (queryStringBuilder.Length > 0)
        //        {
        //            requestUri += "?" + queryStringBuilder.ToString();
        //        }
        //    }

        //    return await this.SendAsync<T>(requestUri, httpMethod, content);
        //}
        //private StringBuilder BuildQueryString(Dictionary<string, object> queryParameters, StringBuilder builder)
        //{
        //    foreach (KeyValuePair<string, object> queryParameter in queryParameters)
        //    {
        //        string queryParameterValue = System.Convert.ToString(queryParameter.Value, CultureInfo.InvariantCulture);
        //        if (!string.IsNullOrWhiteSpace(queryParameterValue))
        //        {
        //            if (builder.Length > 0)
        //            {
        //                builder.Append("&");
        //            }

        //            builder
        //                .Append(queryParameter.Key)
        //                .Append("=")
        //                .Append(HttpUtility.UrlEncode(queryParameterValue));
        //        }
        //    }

        //    return builder;
        //}

        //private string apiKey;
        //private IBinanceSignatureService signatureService;
        //private string baseUrl;
        //private HttpClient httpClient;

        //private async Task<T> SendAsync<T>(string requestUri, HttpMethod httpMethod, object content = null)
        //{
        //    using (var request = new HttpRequestMessage(httpMethod, this.baseUrl + requestUri))
        //    {
        //        if (!(content is null))
        //        {
        //            request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        //        }

        //        if (!(this.apiKey is null))
        //        {
        //            request.Headers.Add("X-MBX-APIKEY", this.apiKey);
        //        }

        //        HttpResponseMessage response = await this.httpClient.SendAsync(request);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            using (HttpContent responseContent = response.Content)
        //            {
        //                string jsonString = await responseContent.ReadAsStringAsync();

        //                if (typeof(T) == typeof(string))
        //                {
        //                    return (T)(object)jsonString;
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        T data = JsonConvert.DeserializeObject<T>(jsonString);

        //                        return data;
        //                    }
        //                    catch (JsonReaderException ex)
        //                    {
        //                        var clientException = new BinanceClientException($"Failed to map server response from '${requestUri}' to given type", -1, ex);

        //                        clientException.StatusCode = (int)response.StatusCode;
        //                        clientException.Headers = response.Headers.ToDictionary(a => a.Key, a => a.Value);

        //                        throw clientException;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            using (HttpContent responseContent = response.Content)
        //            {
        //                BinanceHttpException httpException = null;
        //                string contentString = await responseContent.ReadAsStringAsync();
        //                int statusCode = (int)response.StatusCode;
        //                if (400 <= statusCode && statusCode < 500)
        //                {
        //                    if (string.IsNullOrWhiteSpace(contentString))
        //                    {
        //                        httpException = new BinanceClientException("Unsuccessful response with no content", -1);
        //                    }
        //                    else
        //                    {
        //                        try
        //                        {
        //                            httpException = JsonConvert.DeserializeObject<BinanceClientException>(contentString);
        //                        }
        //                        catch (JsonReaderException ex)
        //                        {
        //                            httpException = new BinanceClientException(contentString, -1, ex);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    httpException = new BinanceServerException(contentString);
        //                }

        //                httpException.StatusCode = statusCode;
        //                httpException.Headers = response.Headers.ToDictionary(a => a.Key, a => a.Value);

        //                throw httpException;
        //            }
        //        }
        //    }
        //}

        #endregion
        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    //Market market = new Market();

        //    //string serverTime = await market.CheckServerTime();

        //    //Console.WriteLine(serverTime);
        //    await FirstTask();
        //    //MessageBox.Show("2");
        //}
        //static async Task FirstTask()
        //{
        //    Market market = new Market();
        //    ///string
        //    serverTime = await market.CheckServerTime();
        //    //Console.WriteLine(serverTime);
        //    //MessageBox.Show(serverTime);
        //}
    }
}