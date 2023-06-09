using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Neoner.Objects;
using UnityEngine.InputSystem;
using TMPro;

namespace Neoner.Mechanics
{

    public class GameMaster : MonoBehaviour
    {
        [Header("Stage Information")]
        [SerializeField]
        private string StageName = "Lorem Ipsum";
        [SerializeField]
        private int StageLevel = 1;
        [SerializeField]
        public NeonColor CurrentColor = NeonColor.LightBlue;
        [Header("List of neon colors")]
        [SerializeField]
        private NeonColor[] _neonColors = new NeonColor[] { NeonColor.Red, NeonColor.Green, NeonColor.LightBlue };
        public NeonColor[] NeonColors { get { return _neonColors; } }
        protected Dictionary<string, GameObject[]> _neons = new Dictionary<string, GameObject[]>();
        private bool isStageComplete = false;

        [Header("Scenes")]
        [Tooltip("Name of the scene to load when player completes the stage. Example: Scenes/Stage2")]
        [SerializeField]
        private string MainMenuSceneName = "Scenes/MainMenu";
        [Tooltip("Name of the scene to load when player completes the stage. Example: Scenes/Stage2")]
        [SerializeField]
        private string NextStageSceneName = "Scenes/Stage2";

        private bool _isPaused = false;

        public void CompleteStage()
        {
            if (isStageComplete)
                return;
            isStageComplete = true;
            Neoner.UI.TimerText timerText = GameObject.FindObjectOfType<Neoner.UI.TimerText>();
            timerText.enabled = false;
            // Find object tagged "Player"
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // Call CompleteStage() method on PlayerController
            player.GetComponent<Neoner.Controller.PlayerController>().CompleteStage();

            // Update canvas
            GameObject canvas = transform.Find("Canvas").gameObject;
            // Update timer text
            canvas.transform.Find("Time").GetComponent<TMP_Text>().text = "Waktu: " + timerText.GetComponent<TMP_Text>().text;

            // Save progress
            Neoner.SaveData.Progress.Save(StageLevel);

            // TODO: Star
            // Parse float from text, mm:ss.ms
            Neoner.SaveData.Level.Save(StageLevel, 1, timerText.CurrentTime);

            // Get best record
            float[] data = Neoner.SaveData.Level.Get(StageLevel);
            if (data != null)
            {
                canvas.transform.Find("Time").GetComponent<TMP_Text>().text = "Rekor: " + string.Format("{0:00}:{1:00}.{2:00}", data[1] / 60, data[1] % 60, (data[1] * 100) % 100);
            }
            else
            {
                canvas.transform.Find("Time").GetComponent<TMP_Text>().text = "Rekor: -";
            }
            canvas.SetActive(true);
            transform.Find("EventSystem").gameObject.SetActive(true);
            // Show cursor
            player.GetComponent<Neoner.InputSystem.PlayerInputs>().cursorLocked = false;
            player.GetComponent<Neoner.InputSystem.PlayerInputs>().cursorInputForLook = false;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Pause()
        {
            if (_isPaused)
            {
                Resume();
                return;
            }
            _isPaused = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Enable EventSystem
            transform.Find("EventSystem").gameObject.SetActive(true);

            // Disable input
            player.GetComponent<Neoner.Controller.PlayerController>().DisableInputs();
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<Neoner.InputSystem.PlayerInputs>().cursorLocked = false;
            player.GetComponent<Neoner.InputSystem.PlayerInputs>().cursorInputForLook = false;

            // Show canvas
            GameObject canvas = transform.Find("PauseMenu").gameObject;
            canvas.SetActive(true);
        }

        public void Resume()
        {
            _isPaused = false;
            // Hide cursor
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Disable EventSystem
            transform.Find("EventSystem").gameObject.SetActive(false);

            // Disable input
            player.GetComponent<Neoner.Controller.PlayerController>().EnableInputs();
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<Neoner.InputSystem.PlayerInputs>().cursorLocked = true;
            player.GetComponent<Neoner.InputSystem.PlayerInputs>().cursorInputForLook = true;

            // Hide canvas
            GameObject canvas = transform.Find("PauseMenu").gameObject;
            canvas.SetActive(false);
        }

        public void RestartStage()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void NextStage()
        {
            // TODO: Next Stage
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(NextStageSceneName);
        }
        public void MainMenu()
        {
            // TODO: Main Menu
            SceneManager.LoadScene(MainMenuSceneName);
        }

        private void GetNeons()
        {
            // Find game object with tag from _neonColors
            // For example, if _neonColors is ["Red", "Green", "LightBlue"]
            // then this will find game objects with tag "Red", "Green", "LightBlue"
            _neons.Clear();
            // foreach (NeonColor color in _neonColors)
            // {
            //     GameObject[] neons = GameObject.FindGameObjectsWithTag("Neon" + color.ToString());
            //     Debug.Log("Found " + neons.Length + " neons with tag Neon" + color.ToString());
            //     _neons.Add(color.ToString(), neons);
            // }
            // Find object by type Neon
            Neon[] neons = GameObject.FindObjectsOfType<Neon>();
            foreach (Neon neon in neons)
            {
                // Check if neon color is in _neonColors
                if (System.Array.IndexOf(_neonColors, neon.Color) == -1)
                    continue;

                if (!_neons.ContainsKey(neon.Color.ToString()))
                    _neons.Add(neon.Color.ToString(), new GameObject[] { neon.gameObject });
                else
                {
                    List<GameObject> list = new List<GameObject>(_neons[neon.Color.ToString()]);
                    list.Add(neon.gameObject);
                    _neons[neon.Color.ToString()] = list.ToArray();
                }
            }
        }

        private void Start()
        {
            GameObject canvas = transform.Find("PauseMenu").gameObject;
            canvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            // Play sound
            AudioSource bgm = transform.Find("BGM").GetComponent<AudioSource>();
            bgm.Play();
            // CompleteStage();
            GetNeons();
            ToggleNeon();
        }
        public virtual void ToggleNeon()
        {
            // Loop through all neons, disable if color is current color, enable if not
            foreach (KeyValuePair<string, GameObject[]> entry in _neons)
            {
                foreach (GameObject neon in entry.Value)
                {
                    Neon neonComponent = neon.GetComponent<Neon>();
                    neonComponent.Init();

                    if (neonComponent.Color == CurrentColor && !neonComponent.IsActive)
                        neonComponent.Toggle();
                    else if (neonComponent.Color != CurrentColor && neonComponent.IsActive)
                        neonComponent.Toggle();
                }
            }
            // UI Player Neon Indicator
            // Find tag
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            if (player.Length == 0)
            {
                Debug.LogError("No player found!");
                return;
            }
            foreach (GameObject p in player)
                p.GetComponent<Neoner.InputSystem.PlayerInputs>().NeonIndicator.GetComponent<Neoner.UI.Neon>().Toggle(CurrentColor);
        }
    }
}