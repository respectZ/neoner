using UnityEngine;
using Neoner.Controller;

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
                PlayerController inputs = other.gameObject.GetComponent<PlayerController>();
                inputs.Warp(_checkpoint.transform.position);
            }
        }
    }
}