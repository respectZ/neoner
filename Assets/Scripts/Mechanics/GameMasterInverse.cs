using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Neoner.Objects;
using UnityEngine.InputSystem;
using TMPro;

namespace Neoner.Mechanics
{
    public class GameMasterInverse : GameMaster
    {
        public override void ToggleNeon()
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