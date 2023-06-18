using UnityEngine;

namespace Neoner.SaveData
{
    public class Progress : MonoBehaviour
    {
        public static int Get()
        {
            if (!PlayerPrefs.HasKey("LastLevel"))
            {
                Debug.Log("No data found for progress");
                return 0;
            }
            return PlayerPrefs.GetInt("LastLevel");
        }
        public static void Save(int stageLevel)
        {
            PlayerPrefs.SetInt("LastLevel", stageLevel + 1);
        }
    }
}