using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using InteractML.Telemetry;

namespace InteractML.DataAugmentation
{
    /// <summary>
    /// Stores telemetry options and details
    /// </summary>
    public class TelemetryDataAugmentation 
    {
        #region Variables

        public string SceneName;
        public string ProjectID;
        
        /// <summary>
        /// How many iterations performed?
        /// </summary>
        public int NumIterations;
        /// <summary>
        /// Number of iterations performed in graph 
        /// </summary>
        public List<IterationDataAugmentation> IMLIterations;

        #endregion

        public TelemetryDataAugmentation()
        {
            IMLIterations = new List<IterationDataAugmentation>();
        }

        public TelemetryDataAugmentation(TelemetryData oldTelemetryFile)
        {
            SceneName = oldTelemetryFile.SceneName;
            ProjectID = oldTelemetryFile.ProjectID;
            NumIterations = oldTelemetryFile.NumIterations;
            IMLIterations = new List<IterationDataAugmentation>();
        }

    }
}