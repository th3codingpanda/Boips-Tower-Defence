using UnityEngine;

namespace EnemyFolder
{
    public class DamageScript : Enemy
    {

        [SerializeField] private int _hp = 100;
        private Renderer _render;
        private PhaseHandler phaseHandler;
        private Color _defColor;
        MoneyHandler moneyHandler;
        [SerializeField]private int money;
        [SerializeField] private AudioSource damageSound;

        public void ColorChange()
        {
            _render.material.color = Color.red;
        }

        void Start()
        {
            phaseHandler = PhaseHandler.Instance;
            moneyHandler = MoneyHandler.Instance;
            _render = GetComponent<Renderer>();
            _defColor = _render.material.color;
        }

        // Update is called once per frame
        void Update()
        {
            //TakeDamage(1);
        }

        public override void Target()
        {
            //ColorChange();
        }
        public override void UnTarget()
        {
            //_render.material.color = _defColor;
        }
        public void TakeDamage(int damage)
        {
            _hp -= damage;
            
            if(_hp <= 0)
            {
                phaseHandler.enemiesOnScreen.Remove(gameObject);
                Destroy(gameObject);
                moneyHandler.ChangeMoney(money);

            }
            else
            {
                damageSound.pitch = Random.Range(0.8f, 1.2f);
                damageSound.Play();
            }
        }

   
    }
}
