using UnityEngine;
using UnityEngine.SceneManagement;

namespace Neoner.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _mainMenu;
        [SerializeField]
        private GameObject _stageSelection;
        private void Start()
        {
            _mainMenu.SetActive(true);
            _stageSelection.SetActive(false);
            InitializeStageSelection();
        }
        private void InitializeStageSelection()
        {
            // Disable all first
            GameObject tutorial = _stageSelection.transform.Find("Tutorial").gameObject;
            GameObject stage1 = _stageSelection.transform.Find("Stage01").gameObject;
            GameObject stage2 = _stageSelection.transform.Find("Stage02").gameObject;
            GameObject stage3 = _stageSelection.transform.Find("Stage03").gameObject;

            GameObject[] stages = new GameObject[] { tutorial, stage1, stage2, stage3 };

            tutorial.SetActive(true);
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(false);

            int lastLevel = Neoner.SaveData.Progress.Get();
            if (lastLevel >= 1)
            {
                stage1.SetActive(true);
            }
            if (lastLevel >= 2)
            {
                stage2.SetActive(true);
            }
            if (lastLevel >= 3)
            {
                stage3.SetActive(true);
            }

            // Level data
            for (int i = 0; i < stages.Length; i++)
            {
                GameObject stage = stages[i];
                if (stage.activeSelf)
                {
                    // Get best record
                    float[] data = Neoner.SaveData.Level.Get(i);
                    if (data == null)
                    {
                        // No data found
                        continue;
                    }
                    // TODO: Star
                    TMPro.TMP_Text timeText = stage.transform.Find("Rekor").GetComponent<TMPro.TMP_Text>();
                    // Update timer text
                    timeText.text = "Rekor: " + string.Format("{0:00}:{1:00}.{2:00}", data[1] / 60, data[1] % 60, (data[1] * 100) % 100);
                }
            }
        }
        public void PlayOnClick()
        {
            // TODO: Stage Selection
            // SceneManager.LoadScene("Scenes/Levels/Tutorial");
            _stageSelection.SetActive(true);
            _mainMenu.SetActive(false);


        }
        public void ExitOnClick()
        {
            Application.Quit();
        }

        public void TutorialOnClick()
        {
            SceneManager.LoadScene("Scenes/Levels/Tutorial");
        }

        public void Stage1OnClick()
        {
            SceneManager.LoadScene("Scenes/Levels/Stage_01");
        }

        public void Stage2OnClick()
        {
            SceneManager.LoadScene("Scenes/Levels/Stage_02");
        }

        public void Stage3OnClick()
        {
            SceneManager.LoadScene("Scenes/Levels/Stage_03");
        }
    }
}