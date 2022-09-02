using System.Collections.Generic;
using UnityEngine;
using InteractML.Telemetry;

namespace InteractML.DataAugmentation
{
    /// <summary>
    /// Useful telemetry data per model per iteration
    /// </summary>
    [System.Serializable]
    public class ModelIterationDataAugmentation
    {
        /// <summary>
        /// Which graph does this model belong to?
        /// </summary>
        public string GraphID;
        /// <summary>
        /// Whic model?
        /// </summary>
        public string ModelID;
        // Training data
        public List<IMLTrainingExample> TrainingData;
        public List<string> TrainingFeatures;
        public List<string> TrainingGameObjects;
        // Live features
        public List<string> FeaturesInUse;
        public List<string> GameObjectsInUse;
        // Testing data
        public List<List<IMLTrainingExample>> TestingData;

        /// <summary>
        /// All possible training features (only gathered if training data collected in this iteration)
        /// </summary>
        public List<FeatureTelemetryWithOutput> AllPossibleTrainingFeaturesData;
        // All possible testing features (only gathered if testing data collected in this iteration)
        /// <summary>
        /// Which ML System nodes are collecting testing features?
        /// </summary>
        public List<FeatureTelemetryWithOutput> AllPossibleTestingFeaturesData;

        public ModelIterationDataAugmentation(ModelIterationData oldModelIterationData)
        {
            GraphID = oldModelIterationData.GraphID;
            ModelID = oldModelIterationData.ModelID;
            TrainingData = oldModelIterationData.TrainingData;
            TrainingFeatures = oldModelIterationData.TrainingFeatures;
            TrainingGameObjects = oldModelIterationData.TrainingGameObjects;
            FeaturesInUse = oldModelIterationData.FeaturesInUse;
            GameObjectsInUse = oldModelIterationData.GameObjectsInUse;
            TestingData = oldModelIterationData.TestingData;
        }
    }
}