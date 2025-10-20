using UnityEngine;

namespace Grid
{
    public class SafetyTeleport : MonoBehaviour
    {
        [SerializeField] private GameObject RespawnPoint;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") )
            {
                other.transform.position = RespawnPoint.transform.position;
            }
        }
    }
}
