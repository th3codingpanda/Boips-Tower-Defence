using UnityEngine;
public class SafetyTeleport : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            other.transform.position = new Vector3(1,0,1);
        }
    }
}
