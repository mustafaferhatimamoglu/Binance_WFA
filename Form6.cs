using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.WindowsAzure.Storage;
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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();

            HousingData[] housingData = new HousingData[]
{
    new HousingData
    {
        Size = 600f,
        HistoricalPrices = new float[] { 100000f, 125000f, 122000f },
        CurrentPrice = 170000f
    },
    new HousingData
    {
        Size = 1000f,
        HistoricalPrices = new float[] { 200000f, 250000f, 230000f },
        CurrentPrice = 225000f
    },
    new HousingData
    {
        Size = 1000f,
        HistoricalPrices = new float[] { 126000f, 130000f, 200000f },
        CurrentPrice = 195000f
    }
};

            // Create MLContext
            MLContext mlContext = new MLContext();

            // Load Data
            IDataView data = mlContext.Data.LoadFromEnumerable<HousingData>(housingData);

            // Define data preparation estimator
            EstimatorChain<RegressionPredictionTransformer<LinearRegressionModelParameters>> pipelineEstimator =
                mlContext.Transforms.Concatenate("Features", new string[] { "Size", "HistoricalPrices" })
                    .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                    .Append(mlContext.Regression.Trainers.Sdca());

            // Train model
            ITransformer trainedModel = pipelineEstimator.Fit(data);

            // Save model
            mlContext.Model.Save(trainedModel, data.Schema, "model.zip");
            //// Create MLContext
            //MLContext mlContext = new MLContext();

            //// Define DataViewSchema of data prep pipeline and trained model
            //DataViewSchema dataPrepPipelineSchema, modelSchema;

            //// Load data preparation pipeline
            //ITransformer dataPrepPipeline = mlContext.Model.Load("data_preparation_pipeline.zip", out dataPrepPipelineSchema);

            //// Load trained model
            //ITransformer trainedModel = mlContext.Model.Load("ogd_model.zip", out modelSchema);
            //Main();
        }

        //static async Task Main()
        //{
        //    string blobData;
        //    var connectionString = "";

        //    string trainerFilePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "models/housing-trainer.zip");
        //    string pipelineFilePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "models/housing-data-prep.zip");
        //    string modelDirectory = Path.Combine(pipelineFilePath, "..");

        //    var storageAccount = CloudStorageAccount.Parse(connectionString);

        //    var client = storageAccount.CreateCloudBlobClient();

        //    var container = client.GetContainerReference("models");

        //    var dataModel = container.GetBlockBlobReference("housing-data-prep.zip");
        //    var trainingModel = container.GetBlockBlobReference("housing-trainer.zip");

        //    if (!File.Exists(pipelineFilePath))
        //    {
        //        if (!Directory.Exists(modelDirectory))
        //        {
        //            Directory.CreateDirectory(modelDirectory);
        //        }

        //        await dataModel.DownloadToFileAsync(pipelineFilePath, FileMode.Create);
        //    }

        //    if (!File.Exists(trainerFilePath))
        //    {
        //        await trainingModel.DownloadToFileAsync(trainerFilePath, FileMode.Create);
        //    }

        //    var context = new MLContext();

        //    DataViewSchema modelSchema, pipelineSchema;

        //    var trainerModel = context.Model.Load(trainerFilePath, out modelSchema);
        //    var dataPrepModel = context.Model.Load(pipelineFilePath, out pipelineSchema);

        //    var originalModelParams =
        //        ((ISingleFeaturePredictionTransformer<object>)trainerModel).Model as PoissonRegressionModelParameters;

        //    var parsedData = File.ReadAllLines("./retrain_housing_data.csv")
        //        .Skip(1)
        //        .Select(line => line.Split(','))
        //        .TakeWhile(row => !string.IsNullOrWhiteSpace(row[0]))
        //        .Select(row => new HousingData
        //        {
        //            Longitude = float.Parse(row[0]),
        //            Latitude = float.Parse(row[1]),
        //            HousingMedianAge = float.Parse(row[2]),
        //            TotalRooms = float.Parse(row[3]),
        //            TotalBedrooms = float.Parse(row[4]),
        //            Population = float.Parse(row[5]),
        //            Households = float.Parse(row[6]),
        //            MedianIncome = float.Parse(row[7]),
        //            MedianHouseValue = float.Parse(row[8]),
        //            OceanProximity = row[9]
        //        });

        //    // Load new data and build new model based off original parameters
        //    var newData = context.Data.LoadFromEnumerable(parsedData);

        //    var newDataTransformed = dataPrepModel.Transform(newData);

        //    var retrainedModel = context.Regression.Trainers.LbfgsPoissonRegression()
        //        .Fit(newDataTransformed, originalModelParams);

        //    // Compare model params
        //    var newModelParams = retrainedModel.Model as PoissonRegressionModelParameters;

        //    var weightDiffs = originalModelParams.Weights.Zip(
        //        newModelParams.Weights, (original, updated) => original - updated).ToArray();

        //    Console.WriteLine("Original\tRetrained\tDifference");
        //    for (int i = 0; i < weightDiffs.Count(); i++)
        //    {
        //        Console.WriteLine($"{originalModelParams.Weights[i]}\t{newModelParams.Weights[i]}\t{weightDiffs[i]}");
        //    }
        //}
    }
}
