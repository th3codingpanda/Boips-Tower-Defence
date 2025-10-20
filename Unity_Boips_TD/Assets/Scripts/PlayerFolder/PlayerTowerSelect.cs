using System.Collections.Generic;
using GridFolder.Towers;
using TMPro;
using UnityEngine;

namespace PlayerFolder
{
    public class PlayerTowerSelect : MonoSingleton<PlayerTowerSelect>
    {
        [SerializeField] private List<GameObject> towers;
        private float currentTowerIndex;
        private InputManager inputscript;
        private UIHandler uihandler;
        [SerializeField]private TextMeshProUGUI towerText;
        TowerPlacementHandler towerPlacementHandler;

        private void Start()
        {
            inputscript = InputManager.Instance;
            towerPlacementHandler = TowerPlacementHandler.Instance;
            uihandler = UIHandler.Instance;
            inputscript.SwapTowerEvent.AddListener(SwapTowers);
            uihandler.ChangeUIText(towerText, $"Current Tower: {towers[(int)currentTowerIndex].name} \nCost: {towers[(int)currentTowerIndex].GetComponent<CostHandler>().cost}");
            towerPlacementHandler.UpdateTowerSelected(towers[(int)currentTowerIndex]);
        }

        private void SwapTowers(float indexChanger)
        {
            if (currentTowerIndex == 0 && indexChanger == -1)
            {
                currentTowerIndex = towers.Count - 1;
            }
            else if (currentTowerIndex == towers.Count - 1  && indexChanger == 1)
            {
                currentTowerIndex = 0;
            }
            else
            {
                currentTowerIndex += indexChanger;
            }
            uihandler.ChangeUIText(towerText, $"Current Tower: {towers[(int)currentTowerIndex].name} \nCost: {towers[(int)currentTowerIndex].GetComponent<CostHandler>().cost}");
            towerPlacementHandler.UpdateTowerSelected(towers[(int)currentTowerIndex]);
            
        }
    }
}