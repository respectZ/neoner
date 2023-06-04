using UnityEngine;

namespace Neoner.Objects
{
    public class DeathArea : MonoBehaviour
    {
        [SerializeField]
        private GameObject _checkpoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // StarterAssets.StarterAssetsInputs inputs = other.gameObject.GetComponent<StarterAssets.StarterAssetsInputs>();
            }
        }
    }
}