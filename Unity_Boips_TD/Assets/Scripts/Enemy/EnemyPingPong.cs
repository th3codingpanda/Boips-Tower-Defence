using UnityEngine;

public class EnemyPingPong : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public Transform startMarker;
    public Transform endMarker;

    void Update()
    {
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(Time.time, 1));
    }


}
