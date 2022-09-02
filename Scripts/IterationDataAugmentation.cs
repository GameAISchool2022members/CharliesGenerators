using System;
using InteractML.Telemetry;


namespace InteractML.DataAugmentation
{
    /// <summary>
    /// Useful telemetry data per iteration
    /// </summary>
    [System.Serializable]
    public class IterationDataAugmentation
    {
        public string GraphID;
        public string SceneName;

        // Time data
        public DateTime StartTimeUTC;
        public DateTime EndTimeUTC;
        public double TotalSeconds;

        /// <summary>
        /// Useful telemetry data per model in graph
        /// </summary>
        public ModelIterationDataAugmentation ModelData;

        public IterationDataAugmentation(IterationData oldIteration)
        {
            GraphID = oldIteration.GraphID;
            SceneName = oldIteration.SceneName;
            StartTimeUTC = oldIteration.StartTimeUTC;
            EndTimeUTC = oldIteration.EndTimeUTC;
            TotalSeconds = oldIteration.TotalSeconds;
            ModelData = new ModelIterationDataAugmentation(oldIteration.ModelData);
            
        }

        /// <summary>
        /// Is there any data contained in this iteration?
        /// </summary>
        /// <returns></returns>
        public bool HasData()
        {
            if (ModelData != null)
            {
                if (ModelData.TrainingData != null && ModelData.TrainingData.Count > 0) return true;
                if (ModelData.TrainingFeatures != null && ModelData.TrainingFeatures.Count > 0) return true;
                if (ModelData.TrainingGameObjects != null && ModelData.TrainingGameObjects.Count > 0) return true;
                // Live features
                if (ModelData.FeaturesInUse != null && ModelData.FeaturesInUse.Count > 0) return true;
                if (ModelData.GameObjectsInUse != null && ModelData.GameObjectsInUse.Count > 0) return true;
                // Testing data
                if (ModelData.TestingData != null && ModelData.TestingData.Count > 0) return true;

                // All possible features
                if (ModelData.AllPossibleTrainingFeaturesData != null && ModelData.AllPossibleTrainingFeaturesData.Count > 0) return true;
                if (ModelData.AllPossibleTestingFeaturesData != null && ModelData.AllPossibleTestingFeaturesData.Count > 0) return true;
            }
            // There is no data if we reach here
            return false;
        }

    }
}