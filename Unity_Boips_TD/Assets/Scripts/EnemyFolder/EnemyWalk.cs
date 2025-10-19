using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace EnemyFolder
{
    public class EnemyWalk : MonoBehaviour
    {
        private List<Vector2> walkwaypoints = new List<Vector2>();
        private EnemyPathGiver pathGiver;
        private PhaseHandler phaseHandler;
        private BaseHandler baseHandler;
        [SerializeField]private int damage;
        [SerializeField]private float speed;
    
        int currentIndex = 0;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            phaseHandler = PhaseHandler.Instance;
            pathGiver = EnemyPathGiver.Instance;
            baseHandler = BaseHandler.Instance;
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
        }

        private void walk()
        {
            float step =  speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(walkwaypoints[currentIndex].x, transform.position.y, walkwaypoints[currentIndex].y),
                step);
            if (transform.position.x == walkwaypoints[currentIndex].x && transform.position.z == walkwaypoints[currentIndex].y)
            {
                if (currentIndex == walkwaypoints.Count - 1)
                {
                    baseHandler.TakeDamage(damage);
                    phaseHandler.enemiesOnScreen.Remove(gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    currentIndex++;
                }
            }
        }
    }
}
