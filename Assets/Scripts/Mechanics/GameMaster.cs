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
        [SerializeField]
        private string StageName = "Lorem Ipsum";
        [SerializeField]
        public NeonColor CurrentColor = NeonColor.LightBlue;
        private NeonColor[] _neonColors = new NeonColor[] { NeonColor.Red, NeonColor.Green, NeonColor.LightBlue };
        protected Dictionary<string, GameObject[]> _neons = new Dictionary<string, GameObject[]>();
        private bool isStageComplete = false;

        [Header("Scenes")]
        [Tooltip("Name of the scene to load when player completes the stage. Example: Scenes/Stage2")]
        [SerializeField]
        private string MainMenuSceneName = "Scenes/MainMenu";
        [Tooltip("Name of the scene to load when player completes the stage. Example: Scenes/Stage2")]
        [SerializeField]
        private string NextStageSceneName = "Scenes/Stage2";

        public void CompleteStage()
        {
            if (isStageComplete)
                return;
            isStageComplete = true;
            Debug.Log("Stage Complete!");
            Neoner.UI.TimerText timerText = GameObject.FindObjectOfType<Neoner.UI.TimerText>();
            timerText.enabled = false;
            // Find object tagged "Player"
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // Call CompleteStage() method on PlayerController
            player.GetComponent<Neoner.Controller.PlayerController>().CompleteStage();
            // TODO: Show complete screen
            GameObject canvas = transform.Find("Canvas").gameObject;
            // Update timer text
            canvas.transform.Find("Time").GetComponent<TMP_Text>().text = "Waktu: " + timerText.GetComponent<TMP_Text>().text;
            // Get children named "Canvas"
            canvas.SetActive(true);
            transform.Find("EventSystem").gameObject.SetActive(true);
            // Show cursor
            player.GetComponent<Neoner.InputSystem.PlayerInputs>().cursorLocked = false;
            player.GetComponent<Neoner.InputSystem.PlayerInputs>().cursorInputForLook = false;
            Cursor.lockState = CursorLockMode.None;
        }

        public void RestartStage()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void NextStage()
        {
            // TODO: Next Stage
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // SceneManager.LoadScene(NextStageSceneName);
        }
        public void MainMenu()
        {
            // TODO: Main Menu
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // SceneManager.LoadScene(MainMenuSceneName);
        }

        private void GetNeons()
        {
            // Find game object with tag from _neonColors
            // For example, if _neonColors is ["Red", "Green", "LightBlue"]
            // then this will find game objects with tag "Red", "Green", "LightBlue"
            _neons.Clear();
            foreach (NeonColor color in _neonColors)
            {
                GameObject[] neons = GameObject.FindGameObjectsWithTag("Neon" + color.ToString());
                Debug.Log("Found " + neons.Length + " neons with tag Neon" + color.ToString());
                _neons.Add(color.ToString(), neons);
            }
        }

        private void Start()
        {
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