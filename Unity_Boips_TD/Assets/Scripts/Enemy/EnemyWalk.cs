using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    private List<Vector2> walkwaypoints = new List<Vector2>();
    private EnemyPathGiver pathGiver;
    [SerializeField]private float speed;
    int currentIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathGiver = EnemyPathGiver.Instance;
        speed = Random.Range(1, 2);
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
                 Destroy(this.gameObject);
             }
             else
             {
                 currentIndex++;
             }
         }
    }
}
