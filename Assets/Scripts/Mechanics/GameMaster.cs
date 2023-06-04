using UnityEngine;
using System.Collections.Generic;
using Neoner.Objects;
using UnityEngine.InputSystem;

namespace Neoner.Mechanics
{

    public class GameMaster : MonoBehaviour
    {
        [SerializeField]
        public NeonColor CurrentColor = NeonColor.LightBlue;
        private NeonColor[] _neonColors = new NeonColor[] { NeonColor.Red, NeonColor.Green, NeonColor.LightBlue };
        private Dictionary<string, GameObject[]> _neons = new Dictionary<string, GameObject[]>();

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
            GetNeons();
            ToggleNeon();
        }
        public void ToggleNeon()
        {
            // Loop through all neons, disable if color is current color, enable if not
            foreach (KeyValuePair<string, GameObject[]> entry in _neons)
            {
                foreach (GameObject neon in entry.Value)
                {
                    Neon neonComponent = neon.GetComponent<Neon>();
                    if (neonComponent.Color == CurrentColor && neonComponent.IsActive)
                        neonComponent.Toggle();
                    else if (neonComponent.Color != CurrentColor && !neonComponent.IsActive)
                        neonComponent.Toggle();
                }
            }
        }
    }
}