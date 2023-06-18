using UnityEngine;

namespace Neoner.SaveData
{
    public class Level : MonoBehaviour
    {
        /// <summary>
        /// Get save data for a level.
        /// <example>
        /// For example:
        /// <code>
        /// GetLevelData("Stage1");
        /// </code>
        /// </example>
        /// <returns>Array of float containing star and time data.</returns>
        /// </summary>
        static public float[] Get(int stageLevel)
        {
            if (!PlayerPrefs.HasKey(stageLevel + "Star") || !PlayerPrefs.HasKey(stageLevel + "Time"))
            {
                Debug.Log("No data found for level " + stageLevel);
                return null;
            }
            int star = PlayerPrefs.GetInt(stageLevel + "Star");
            float time = PlayerPrefs.GetFloat(stageLevel + "Time");
            return new float[] { star, time };
        }

        static public void Save(int stageLevel, int star, float time)
        {
            float[] data = Get(stageLevel);
            if (data != null)
            {
                if (data[0] > star)
                    star = (int)data[0];
                if (data[1] < time)
                    time = data[1];
            }
            PlayerPrefs.SetInt(stageLevel + "Star", star);
            PlayerPrefs.SetFloat(stageLevel + "Time", time);
        }
    }
}