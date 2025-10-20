
using System;
using UnityEngine;

namespace Grid.Towers
{
    public class TowerOnWallPlacement : MonoBehaviour
    {
        // Update is called once per frame
        public bool towerPlaced;
        MoneyHandler moneyHandler;

        private void Start()
        {
            moneyHandler = MoneyHandler.Instance;
        }

        public void PlaceTower(GameObject tower, float money)
        {
            if (moneyHandler.CheckMoneyAmount(money))
            {
                if (!towerPlaced)
                {
                    moneyHandler.ChangeMoney(-money);
                    towerPlaced = true;
                    GameObject PlacedTower = Instantiate(tower,
                        transform.position + new Vector3(0,
                            transform.localScale.y / 2 + tower.transform.localScale.y / 2, 0), Quaternion.identity);
                    PlacedTower.transform.SetParent(transform);
                    Debug.Log("Place Tower" + tower.transform.position);
                }
            }
            else
            {
                Debug.Log("insufficient money");
            }
        }
    }
}
