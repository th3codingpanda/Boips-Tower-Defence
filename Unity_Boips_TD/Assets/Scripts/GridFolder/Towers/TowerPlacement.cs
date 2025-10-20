
using UnityEngine;

namespace Grid.Towers
{
    public class TowerPlacement : MonoBehaviour
    {
        // Update is called once per frame
        bool towerPlaced;
        public void PlaceTower(GameObject tower)
        {
            if (!towerPlaced)
            {
                towerPlaced = true;
                Instantiate(tower,transform.position + new Vector3(0,transform.localScale.y / 2 + tower.transform.localScale.y,0), Quaternion.identity);
            }
        }
    }
}
