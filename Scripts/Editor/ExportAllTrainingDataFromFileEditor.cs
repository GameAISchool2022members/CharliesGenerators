using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace InteractML.DataAugmentation
{
    [CustomEditor(typeof(ExportAllTrainingDataFromFile))]
    public class ExportAllTrainingDataFromFileEditor : Editor
    {
        ExportAllTrainingDataFromFile exportScript;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (exportScript == null) exportScript = target as ExportAllTrainingDataFromFile;

            // BUTTONS
            if (GUILayout.Button("Export data from all files"))
            {
                //exportScript.ExtractTrainingDataOfTelemetryFiles(exportScript.WhichFileToProcess, exportScript.TelemetryFileReader.TelemetryFiles);
                exportScript.ExportTrainingDataOfAllTelemetryFiles(exportScript.TelemetryFileReader.TelemetryFiles);
            }
        }
    }

}
