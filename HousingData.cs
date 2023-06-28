namespace Binance_WFA
{
    internal class HousingData
    {
        //public float Longitude { get; set; }
        //public float Latitude { get; set; }
        //public float HousingMedianAge { get; set; }
        //public float TotalRooms { get; set; }
        //public float TotalBedrooms { get; set; }
        //public float Population { get; set; }
        //public float Households { get; set; }
        //public float MedianIncome { get; set; }
        //public float MedianHouseValue { get; set; }
        //public string OceanProximity { get; set; }
        public float Size { get; internal set; }
        public float []Features { get; internal set; }
        public float[] HistoricalPrices { get; internal set; }
        public float CurrentPrice { get; internal set; }
    }
}