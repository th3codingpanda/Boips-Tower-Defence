
using UnityEngine;

namespace Grid.Towers
{
    public class TowerPlacement : MonoBehaviour
    {
        // Update is called once per frame
        private float timer;
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 10)
            {
                timer = 0;
                this.transform.position += new Vector3(0,1,0);
            }

       
        
        }
    }
}
