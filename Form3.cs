﻿using Binance.Spot;
using Binance.Spot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Binance_WFA
{
    public partial class Form3 : Form
    {
        public readonly BackgroundWorker bgW_BTCUSDT = new();
        long Time_2022 = 1640995200000;
        long Time_1000_minutes = 60000000;
        string SQLCon;
        public Form3()
        {
            InitializeComponent();

            //SQLCon ="Data Source=" + "localhost" + "," + "1433" +
            //        "; Initial Catalog=" + "BINANCE" +
            //        "; USER ID=" + "sa" +
            //        ";PASSWORD=" + "sapass" + "";
            SQLCon = "Data Source=" + "localhost" +
                    "; Initial Catalog=" + "BINANCE" +
                    "; USER ID=" + "sa" +
                    ";PASSWORD=" + "sapass" + "";
            try
            {
                var gettime = "\r\n  SELECT TOP 1 Kline_open_time\r\n  FROM [BINANCE].[dbo].[BTCUSDT]\r\n  order by  Kline_open_time desc";
                var a1 = SQL_query(gettime);
                var a2 = a1.Rows[0][0];
                Time_2022 = (long)System.Convert.ToDouble(a2);
            }
            catch (Exception ex)
            {

                throw;
            }

            bgW_BTCUSDT.DoWork += new System.ComponentModel.DoWorkEventHandler(bgW_BTCUSDT_DoWorkAsync);
            bgW_BTCUSDT.RunWorkerAsync();
        }
        Market market = new Market();
        private async void bgW_BTCUSDT_DoWorkAsync(object? sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                Time_2022 = Time_2022 + (i*Time_1000_minutes);
                var RawData = await market.KlineCandlestickData(
                    "BTCUSDT",
                    Interval.ONE_MINUTE,
                    Time_2022,
                    null,
                    1000);
                var RawData_1 = RawData.Replace("],[", "\n").
                    Replace("[[", "").
                    Replace("]]", "").
                    Replace("\"", "");
                var DataLine = RawData_1.Split('\n');
                foreach (var line in DataLine)
                {
                    var column = line.Split(",");
                    string sqlQuery = "" +
                        "INSERT INTO [dbo].[BTCUSDT]\r\n" +

                        "([Kline_open_time]\r\n,[Open_price]\r\n," +
                        "[High_price]\r\n,[Low_price]\r\n," +
                        "[Close_price]\r\n,[Volume]\r\n," +
                        "[Kline_close_time]\r\n,[Quote_asset_volume]\r\n," +
                        "[Number_of_trades]\r\n,[Taker_buy_base_asset_volume]\r\n," +
                        "[Taker_buy_quote_asset_volume])\r\n" +
                        "VALUES\r\n" +
                        "(" + column[0] + "\r\n," + column[1] + "\r\n," +
                        "" + column[2] + " \r\n," + column[3] + "\r\n," +
                        "" + column[4] + "\r\n," + column[5] + "\r\n," +
                        "" + column[6] + "\r\n," + column[7] + "\r\n," +
                        "" + column[8] + "\r\n," + column[9] + "\r\n," +
                        "" + column[10] + ")";
                    SQL_query(sqlQuery);
                }
            }
        }
         async void GetData()
        {
            
        }
        public DataTable SQL_query(string sqlSorgu)//Databaseden verileri datatale olarak döndürür
        {
            //using (SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings[connectName].ConnectionString))
            //using (SqlConnection connect = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=MERPEY;User ID=sa;Password=Ferhat_123*;Connect Timeout=60;Persist Security Info=True;MultipleActiveResultSets=true;"))// providerName = \"System.Data.SqlClient"))
            using (SqlConnection connect = new SqlConnection(SQLCon+ ";Connect Timeout=60;Persist Security Info=True;MultipleActiveResultSets=true;"))// providerName = \"System.Data.SqlClient"))
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlSorgu, connect))
                {
                    try
                    {
                        da.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        //Logger.Logcu(ex + "--Sorgu: " + sqlSorgu);
                        return dt;
                    }
                }
            }
        }
        
        void ParseKlineData(string gelen)
        {

        }
    }
}
