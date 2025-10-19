using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    private List<Vector2> walkwaypoints = new List<Vector2>();
    private EnemyPathGiver pathGiver;
    [SerializeField]private float speed;
    private PhaseHandler phaseHandler;
    int currentIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phaseHandler = PhaseHandler.Instance;
        pathGiver = EnemyPathGiver.Instance;
        if (speed == 0)
        {
            speed = Random.Range(1, 3);
        }
      getpath();
    }

    // Update is called once per frame
    void Update()
    {
        walk();
    }
    private void getpath()
    {
        walkwaypoints = pathGiver.GetEnemyPath();
        Debug.Log(walkwaypoints.Count);
        Debug.Log(walkwaypoints[0]);
    }

    private void walk()
    {
         float step =  speed * Time.deltaTime;
         this.transform.position = Vector3.MoveTowards(this.transform.position,
             new Vector3(walkwaypoints[currentIndex].x, this.transform.position.y, walkwaypoints[currentIndex].y),
             step);
         if (this.transform.position.x == walkwaypoints[currentIndex].x && this.transform.position.z == walkwaypoints[currentIndex].y)
         {
             if (currentIndex == walkwaypoints.Count - 1)
             {
                 phaseHandler.enemiesOnScreen.Remove(gameObject);
                 Destroy(this.gameObject);
             }
             else
             {
                 currentIndex++;
             }
         }
    }
}
