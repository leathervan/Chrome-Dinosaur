using UnityEngine;

namespace Dino.Scripts
{
    public static class ScoreCounter
    {
        private static float startTime;
        private static float endTime;
        private static float elapsedTime;

        private static bool isRunning;

        private static float allTimeScore;
        private static float currentScore;

        public static void Start()
        {
            startTime = Time.time;
            isRunning = true;
        }

        public static void Stop()
        {
            endTime = Time.time;
            elapsedTime = endTime - startTime;
            isRunning = false;
        }

        public static void Update()
        {
            if (isRunning)
            {
                elapsedTime = Time.time - startTime;
                currentScore = Mathf.RoundToInt(elapsedTime * 10);
                if (currentScore > allTimeScore) allTimeScore = currentScore;
            }
        }

        public static float GetCurrentScore()
        {
            return currentScore;
        }

        public static float GetAllTimeScore()
        {
            return allTimeScore;
        }
        
        public static void SetAllTimeScore(float score)
        {
            allTimeScore = score;
        }
    }
}