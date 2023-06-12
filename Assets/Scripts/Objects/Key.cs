using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neoner.Objects
{
    public class Key : MonoBehaviour
    {
        [SerializeField]
        private bool pickupTriggerComplete = false;

        [SerializeField]
        private float rotationSpeed = 0.1f;
        [SerializeField]
        private float floatSpeed = 1f;
        [SerializeField]
        private float floatHeight = 0.1f;

        private void Update()
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * floatSpeed) * floatHeight + transform.position.y, transform.position.z);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                // other.gameObject.GetComponent<PlayerController>().hasKey = true;
                if (pickupTriggerComplete)
                {
                    // Find object tagged "GameMaster"
                    GameObject gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
                    // Call CompleteStage() method on GameMaster
                    gameMaster.GetComponent<Neoner.Mechanics.GameMaster>().CompleteStage();
                }
                Destroy(gameObject);
            }
        }
    }
}