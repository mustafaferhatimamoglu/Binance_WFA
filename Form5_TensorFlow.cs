using NumSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow;
//using DataType = Tensorflow.DataType;
//using Tensorflow;
using Tensorflow.Keras.Layers;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace Binance_WFA
{
    public partial class Form5_TensorFlow : Form
    {
        public Form5_TensorFlow()
        {
            //InitializeComponent();

            //// Load the historical stock price data
            //float[] times = LoadTimes();
            //float[] highs = LoadHighPrices();
            //float[] lows = LoadLowPrices();

            //// Define the hyperparameters
            //int inputSize = 10; // Number of previous data points to consider as input
            //int numHiddenUnits = 64;
            //int numOutputUnits = 1;
            //int numSteps = times.Length - inputSize;

            //// Prepare the input and target data
            //float[,] input = new float[numSteps, inputSize * 2]; // Two features: high and low prices
            //float[,] target = new float[numSteps, numOutputUnits];

            //for (int i = 0; i < numSteps; i++)
            //{
            //    for (int j = 0; j < inputSize; j++)
            //    {
            //        input[i, j] = highs[i + j];
            //        input[i, j + inputSize] = lows[i + j];
            //    }
            //    target[i, 0] = highs[i + inputSize];
            //}

            //// Build the TensorFlow model
            //Graph graph = new Graph();
            //Session session = new Session(graph);

            //// Define the placeholders for input and target data
            //Shape inputShape = new Shape(-1, inputSize * 2);
            //Tensor inputTensor = graph.Placeholder(DataType.Float, inputShape);
            //Shape targetShape = new Shape(-1, numOutputUnits);
            //Tensor targetTensor = graph.Placeholder(DataType.Float, targetShape);

            //// Add the LSTM layer
            //LSTMCell lstmCell = new LSTMCell(graph, numHiddenUnits);
            //Operations lstmOutput = lstmCell.Apply(inputTensor);

            //// Add a dense output layer
            //Operations output = graph.Dense(lstmOutput.Output, numOutputUnits);

            //// Define the loss function (mean squared error)
            //Operations loss = graph.ReduceMean(graph.Square(output - targetTensor));

            //// Define the optimizer (e.g., Adam)
            //Output optimizer = graph.AdamOptimizer().Minimize(loss);

            //// Initialize variables and start the training loop
            //session.Run(graph.Initializer);

            //for (int epoch = 0; epoch < 100; epoch++)
            //{
            //    session.Run(optimizer, new Tensor[] { input }, new Tensor[] { target });
            //    float lossValue = session.Run(loss, new Tensor[] { input }, new Tensor[] { target }).GetValue<float>();
            //    Console.WriteLine($"Epoch: {epoch + 1}, Loss: {lossValue}");
            //}

            //// Make predictions
            //float[] inputSequence = new float[inputSize * 2];
            //Array.Copy(highs, highs.Length - inputSize, inputSequence, 0, inputSize);
            //Array.Copy(lows, lows.Length - inputSize, inputSequence, inputSize, inputSize);
            //Tensor inputSequenceTensor = Tensor.FromBuffer(new Shape(1, inputSize * 2), inputSequence);
            //float[] predictedPrices = session.Run(output, new Tensor[] { inputSequenceTensor }).GetValue<float[]>();

            //// Print the predicted prices
            //Console.WriteLine("Predicted Prices:");
            //foreach (float price in predictedPrices)
            //{
            //    Console.WriteLine(price);
            //}
        }
        static float[] LoadTimes()
        {
            // Load historical time data from a file or any other source
            // Return the data as a float array
            // Example data for demonstration purposes
            return new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f };
        }

        static float[] LoadHighPrices()
        {
            // Load historical high price data from a file or any other source
            // Return the data as a float array
            // Example data for demonstration purposes
            return new float[] { 100.0f, 105.0f, 98.0f, 110.0f, 115.0f, 105.0f, 120.0f, 125.0f };
        }

        static float[] LoadLowPrices()
        {
            // Load historical low price data from a file or any other source
            // Return the data as a float array
            // Example data for demonstration purposes
            return new float[] { 90.0f, 95.0f, 88.0f, 100.0f, 105.0f, 95.0f, 110.0f, 115.0f };
        }
    }
}
