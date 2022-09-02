using InteractML.Telemetry;

namespace InteractML.DataAugmentation
{
    [System.Serializable]
    public class FeatureTelemetryWithOutput 
    {
        public string FeatureName;
        public string GameObject;
        public double[] Data;
        public double[] Output;

        public FeatureTelemetryWithOutput(FeatureTelemetry oldFeature, double[] output)
        {
            this.FeatureName = oldFeature.FeatureName;
            this.GameObject = oldFeature.GameObject;
            Data = new double[oldFeature.Data.Length];
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = oldFeature.Data[i];
            }
            this.Output = output;

        }
    }
}