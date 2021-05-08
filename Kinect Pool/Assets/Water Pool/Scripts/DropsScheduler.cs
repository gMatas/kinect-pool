using UnityEngine;

namespace Assets.WaterPool.Scripts
{
    public class DropsScheduler : MonoBehaviour
    {
        public int minClusterSize = 1;
        public int maxClusterSize = 3; 
        public float clusterDropWaitTime = 15;
        public float meanDropWaitTime = 1;
        public float dropWaitTimeStd = 1;
        public bool dropOnStart = true;

        private float _dropTime;  // when to drop?
        private Vector2[] _dropPointsCluster;
        private float[] _clusterWaitTimes;
        private int _dropIndex;

        private WaterSimulation _waterSimulation;

        void Start()
        {
            _waterSimulation = GetComponent<WaterSimulation>();

            UpdateSchedule();
            _dropTime = dropOnStart ? 0f : Time.time + clusterDropWaitTime;
        }

        void Update()
        {
            float currentTime = Time.time;
            if (currentTime < _dropTime)
                return;

            if (_dropPointsCluster.Length > 0)
            {
                Vector2 dropPoint = _dropPointsCluster[_dropIndex];
                _waterSimulation.AddWave(dropPoint);
            }
            
            if (++_dropIndex >= _dropPointsCluster.Length)
                UpdateSchedule();

            if (_clusterWaitTimes.Length > 0)
                _dropTime = currentTime + _clusterWaitTimes[_dropIndex];
            else
                _dropTime = currentTime + clusterDropWaitTime;
        }

        public static Vector2[] GetRandomDropPoints(int n, Rect[] samplingRegions)
        {
            Vector2[] points = new Vector2[n];
            for (int i = 0; i < n; i++)
            {
                Rect region = samplingRegions[Random.Range(0, samplingRegions.Length)];
                float x = Random.Range(region.xMin, region.xMax);
                float y = Random.Range(region.yMin, region.yMax);
                points[i] = new Vector2(x, y);
            }
            return points;
        }

        private void UpdateSchedule()
        {
            int nDrops = Random.Range(minClusterSize, maxClusterSize + 1);
            if (nDrops == 0)
                return;

            _dropPointsCluster = GetRandomDropPoints(nDrops, new Rect[] {
                new Rect(0, 0, 1, 0),
                new Rect(1, 0, 0, 1),
                new Rect(0, 1, 1, 0),
                new Rect(0, 0, 0, 1)
            });
            _clusterWaitTimes = new float[nDrops];
            _clusterWaitTimes[0] = clusterDropWaitTime;
            for (int i = 1; i < nDrops; i++)
            {
                float waitTime = Mathf.Abs(NormalizedRandom(meanDropWaitTime, dropWaitTimeStd));
                _clusterWaitTimes[i] = waitTime;
            }
            _dropIndex = 0;
        }

        private static float NormalizedRandom(float mu, float sigma)
        {
            // Box-Muller transform is good for a quick-and-dirty solution.
            // https://stackoverflow.com/questions/218060/random-gaussian-variables

            float u1 = 1.0f - Random.Range(0f, 1f); //uniform(0,1] random doubles
            float u2 = 1.0f - Random.Range(0f, 1f);
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
            float value = mu + sigma * randStdNormal; //random normal(mean,stdDev^2)

            return value;
        }
    }
}