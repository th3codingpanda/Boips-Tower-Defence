using UnityEngine;

public class BaseDefense : MonoBehaviour
{

    [SerializeField] private int hp = 100;
   
    void Start()
    {
        
    }


    void Update()
    {
        //OnCollisionEnter();
    }

    private void OnCollisionEnter(Collision col)
    {
        //Debug.Log("spoon " + col.gameObject);

        if (col.gameObject.CompareTag("Enemy"))
        {
            hp -= 10;

            Debug.Log($"spoon + {col.gameObject.name}");
            Destroy(GameObject.FindWithTag("Enemy"));
        }
    }

  
}
