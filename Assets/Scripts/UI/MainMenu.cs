using UnityEngine;
using UnityEngine.SceneManagement;

namespace Neoner.UI
{
    public class MainMenu : MonoBehaviour
    {
        public static void PlayOnClick()
        {
            // TODO: Stage Selection
            SceneManager.LoadScene("Scenes/Levels/Tutorial");
        }
        public static void ExitOnClick()
        {
            Application.Quit();
        }
    }
}