using UnityEngine;

namespace Neoner.Objects
{
    public class Neon : MonoBehaviour
    {
        [SerializeField]
        NeonColor color;
        public NeonColor Color { get { return color; } }

        private BoxCollider boxCollider;
        [SerializeField]
        private ParticleSystem _particleSystem;
        private bool isActive = true;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }
        private void Start()
        {
            boxCollider = gameObject.GetComponent<BoxCollider>();
        }

        public void Toggle()
        {
            // Debug.Log("Toggling neon " + color.ToString());
            isActive = !isActive;
            boxCollider.enabled = isActive;
            // Debug.Log("Neon " + color.ToString() + " is now " + (isActive ? "active" : "inactive"));
            // Toggle particle system
            if (_particleSystem != null)
            {
                if (isActive)
                {
                    _particleSystem.Play();
                }
                else
                {
                    _particleSystem.Stop();
                }
            }
        }
    }

    public enum NeonColor
    {
        Red,
        Green,
        LightBlue,
        Yellow,
        Purple,
    }
}