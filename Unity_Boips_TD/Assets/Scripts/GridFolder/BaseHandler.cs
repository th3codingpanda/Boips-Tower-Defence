using System;
using TMPro;
using UnityEngine;

namespace Grid
{
    public class BaseHandler : MonoSingleton<BaseHandler>
    {

        [SerializeField] public int baseHp = 100;
        private UIHandler uiHandler;
        [SerializeField]private TextMeshProUGUI baseText;
        private bool shownloss;

        public WinLoseUi LoseUI;
      

        void Start()
        {
            uiHandler = UIHandler.Instance;
            uiHandler.ChangeUIText(baseText, $"BaseHP: {baseHp}");
        }

        public void TakeDamage(int damage)
        {
            baseHp -= damage;
            uiHandler.ChangeUIText(baseText, $"BaseHP: {baseHp}");
        }

        private void Update()
        {
            if (baseHp <= 1 && !shownloss)
            {
                shownloss = true;
                Debug.Log("loss");
                // Some thing to make it show u lost
                LoseUI.LoseGame();
            }
        }
    }
}
