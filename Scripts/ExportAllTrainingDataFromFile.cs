using ReusableMethods;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InteractML.Telemetry;
using InteractML;

namespace InteractML.DataAugmentation
{
    /// <summary>
    /// Gets all possible features from a telemetry file and exports them into another file
    /// </summary>
    public class ExportAllTrainingDataFromFile : MonoBehaviour
    {
        #region Variables

        public TelemetryReader TelemetryFileReader { get => m_TelemetryFileReader; }
        [SerializeField]
        private TelemetryReader m_TelemetryFileReader;


        /// <summary>
        /// Path to folder with exported info
        /// </summary>
        public string PathFolderToExport;
        /// <summary>
        /// FileName to export
        /// </summary>
        public string FileNameToExport;

        public int WhichFileToProcess { get => m_WhichFileToProcess; set => m_WhichFileToProcess = value; }
        private int m_WhichFileToProcess;

        /// <summary>
        /// This is the new telemetry data where all possible features are labelled 
        /// </summary>
        private List<TelemetryDataAugmentation> m_NewTelemetryFilesPerScene;

        #endregion


        #region Accuracy calculations

        /// <summary>
        /// Calculate the average accuracy from all iterations in a telemetryFile
        /// </summary>
        /// <param name="oldTelemetryFile"></param>
        /// <returns></returns>
        private bool ExportTrainingDataPerIteration(TelemetryData oldTelemetryFile, ref TelemetryDataAugmentation newExportedFile)
        {
            // If all is good with telemetry file
            if (oldTelemetryFile != null && !Lists.IsNullOrEmpty(ref oldTelemetryFile.IMLIterations))
            {
                // If there isn't a single iteration with all possible features and training data, abort!
                if (!oldTelemetryFile.IMLIterations.Where(x => x.ModelData.AllPossibleTrainingFeaturesData != null && x.ModelData.TrainingData != null).Any())
                {
                    Debug.LogError("All possible features and training data are always null in all iterations of selected file!");
                    return false;
                }

                // how many models are included in this file
                List<string> modelIDs = new List<string>();

                List<IterationDataAugmentation> iterations = new List<IterationDataAugmentation>();
                // Iterate through IML iterations and 'construct' a complete iteration separated by models
                foreach (var IMLIteration in oldTelemetryFile.IMLIterations)
                {
                    // if we got all possible features, separate them into its own list
                    if (!Lists.IsNullOrEmpty(ref IMLIteration.ModelData.AllPossibleTrainingFeaturesData) && !Lists.IsNullOrEmpty(ref IMLIteration.ModelData.TrainingData))
                    {
                        // new empty iteration for augmentation
                        IterationDataAugmentation newIteration = new IterationDataAugmentation(IMLIteration);
                        newIteration.ModelData.AllPossibleTrainingFeaturesData = new List<FeatureTelemetryWithOutput>();

                        // Prepare to assign outputs to all positions collected
                        int countTrainingData = IMLIteration.ModelData.TrainingData.Count;
                        int countAllFeatures = IMLIteration.ModelData.AllPossibleTrainingFeaturesData.Count;
                        int allFeaturesPerTrainingData = IMLIteration.ModelData.AllPossibleTrainingFeaturesData.Count / IMLIteration.ModelData.TrainingData.Count;
                        Debug.Log($"Iteration has {countTrainingData} Training Examples and {countAllFeatures} Possible Features. Divided allFeatures between tData = {allFeaturesPerTrainingData}");

                        // We will constraint the search for the first n features at a time
                        int windowMax = allFeaturesPerTrainingData;
                        // go through each n features and assign them the output from the training data
                        for (int i = 0; i < windowMax; i++)
                        {
                            var output = IMLIteration.ModelData.TrainingData[i].GetOutputs();
                            var feature = IMLIteration.ModelData.AllPossibleTrainingFeaturesData[i];
                            bool isPosition = false;
                            // Only save positions
                            if (feature != null)
                                isPosition = feature.FeatureName.Equals("Position");
                            if (isPosition)
                            {
                                FeatureTelemetryWithOutput positionFeatureToExport = new FeatureTelemetryWithOutput(feature, output);
                                newIteration.ModelData.AllPossibleTrainingFeaturesData.Add(positionFeatureToExport);
                            }

                            // end of window, increase window boundaries to process next n features up to the max amount of features in iteration
                            if (i == windowMax - 1)
                            {
                                windowMax += allFeaturesPerTrainingData;
                                if (windowMax > countAllFeatures) windowMax = countAllFeatures;
                            }
                        }

                        // add iterations to new 
                        if (newExportedFile != null)
                        {
                            if (newExportedFile.IMLIterations == null) newExportedFile.IMLIterations = new List<IterationDataAugmentation>();
                            // we have a new iteration ready to save in a list of iterations
                            newExportedFile.IMLIterations.Add(newIteration);
                        }
                    }

                }
            }

            return true;
        }

        /// <summary>
        /// Calculates the accuracy of one file
        /// </summary>
        /// <param name="whichFile"></param>
        public void ExportTrainingDataOfTelemetryFiles(int whichFile, List<TelemetryData> telemetryFiles)
        {
            // if not null, empty and with enough files
            if (telemetryFiles != null && telemetryFiles.Count > 0 && telemetryFiles.Count >= whichFile + 1)
            {
                var oldFile = telemetryFiles[whichFile];
                TelemetryDataAugmentation newFile = new TelemetryDataAugmentation(oldFile);
                if (oldFile != null)
                {
                    ExportTrainingDataPerIteration(oldFile, ref newFile);
                }
            }
        }

        public void ExportTrainingDataOfAllTelemetryFiles(List<TelemetryData> oldTelemetryFiles, bool clearPreviousData = false)
        {
            // if not null and empty 
            if (oldTelemetryFiles != null && oldTelemetryFiles.Count > 0)
            {
                TelemetryDataAugmentation newFile = new TelemetryDataAugmentation();

                foreach (var oldFile in oldTelemetryFiles)
                {
                    if (oldFile != null)
                    {
                        if (string.IsNullOrEmpty(newFile.SceneName))
                        {
                            newFile.SceneName = oldFile.SceneName;
                            newFile.ProjectID = oldFile.ProjectID;
                        }
                        else if (!newFile.SceneName.Equals(oldFile.SceneName))
                        {
                            // new scene, we need to save the previous file and create a new file
                            var auxFiletoSave = newFile;
                            newFile = null;
                            newFile = new TelemetryDataAugmentation(oldFile);
                            IMLDataSerialization.SaveObjectToDiskAsync(auxFiletoSave, PathFolderToExport, $"{auxFiletoSave.SceneName}_augmentationData.json");
                        }

                        ExportTrainingDataPerIteration(oldFile, ref newFile);
                    }
                }
            }

        }

        #endregion

    }

}
