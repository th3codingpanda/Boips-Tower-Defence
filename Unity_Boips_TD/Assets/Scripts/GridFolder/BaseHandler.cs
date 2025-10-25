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
    }
}
