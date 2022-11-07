using Microsoft.ML;
using PlexShareDashboard.Dashboard.Server.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlexShareFacts.DashboardFacts.Summary
{
    public class SentimentAnalyzerFacts
    {

        MLContext _mlContext;
        ITransformer _trainedModel;

        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"../../../../SentimentModel/SentimentModel.Model/MLModel.zip";

        private const string UNIT_Fact_DATA_FILEPATH = @"../../../unit_Fact_data_baseline.tsv";

        private const string EVALUATION_DATA_FILEPATH = @"../../../evaluation_dataset.tsv";

        public SentimentAnalyzerFacts()
        {
            _mlContext = new MLContext();

            _trainedModel = _mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out var modelInputSchema);
        }

        [Fact]
        public void FactPositiveSentimentStatement()
        {
            ModelInput sampleStatement = new ModelInput { Text = "ML.NET is awesome!" };

            var predEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_trainedModel);

            var resultprediction = predEngine.Predict(sampleStatement);

            Assert.Equal(true, Convert.ToBoolean(resultprediction.Prediction));
        }

        [Fact]
        public void FactNegativeSentimentStatement()
        {
            string FactStatament = "This movie was very boring...";
            ModelInput sampleStatement = new ModelInput { Text = FactStatament };

            var predEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_trainedModel);

            var resultprediction = predEngine.Predict(sampleStatement);

            Assert.Equal(false, Convert.ToBoolean(resultprediction.Prediction));
        }


        [Fact]
        public void FactAccuracyHigherThan60()
        {
            Console.WriteLine("===== Evaluating Model's accuracy with Evaluation/Fact dataset =====");

            // Read dataset to get a single row for trying a prediction          
            IDataView FactDataView = _mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: GetAbsolutePath(EVALUATION_DATA_FILEPATH),
                                            hasHeader: true,
                                            separatorChar: '\t');

            IEnumerable<ModelInput> samplesForPrediction = _mlContext.Data.CreateEnumerable<ModelInput>(FactDataView, false);

            //DO BULK PREDICTIONS
            IDataView predictionsDataView = _trainedModel.Transform(FactDataView);

            var predictions = _trainedModel.Transform(FactDataView);
            var metrics = _mlContext.BinaryClassification.Evaluate(data: predictionsDataView, labelColumnName: "sentiment", scoreColumnName: "Score");

            double accuracy = metrics.Accuracy;
            Console.WriteLine($"Accuracy of model in this validation '{accuracy * 100}'%");

            Assert.True(accuracy >=0.80);
        }

        /*
        //Generate many Fact cases with a bulk prediction approach
        public static List<FactCaseData> FactCases
        {
            get
            {
                MLContext mlContext = new MLContext();
                ITransformer trainedModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out var modelInputSchema);

                // Read dataset to get a single row for trying a prediction          
                IDataView FactDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                                path: GetAbsolutePath(UNIT_Fact_DATA_FILEPATH),
                                                hasHeader: true,
                                                separatorChar: '\t');

                IEnumerable<ModelInput> samplesForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(FactDataView, false);
                ModelInput[] arraysamplesForPrediction = samplesForPrediction.ToArray();

                //DO BULK PREDICTIONS
                IDataView predictionsDataView = trainedModel.Transform(FactDataView);
                IEnumerable<ModelOutput> predictions = mlContext.Data.CreateEnumerable<ModelOutput>(predictionsDataView, false);
                ModelOutput[] arrayPredictions = predictions.ToArray();

                var FactCases = new List<FactCaseData>();

                for (int i = 0; i < arraysamplesForPrediction.Length; i++)
                {
                    FactCases.Add(new FactCaseData(arraysamplesForPrediction[i].Text,
                                                   arrayPredictions[i].Prediction,
                                                   arraysamplesForPrediction[i].Sentiment));
                }

                return FactCases;
            }
        }

        [FactCaseSource("FactCases")]
        public void FactSentimentStatement(string sampleText, bool predictedSentiment, bool expectedSentiment)
        {
            try
            {
                Console.WriteLine($"Text {sampleText} predicted as {predictedSentiment} should be {expectedSentiment}");
                Assert.Equal(predictedSentiment, expectedSentiment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        */

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(SentimentAnalyzerFacts).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
