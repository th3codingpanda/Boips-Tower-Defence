using EnemyFolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerFolder
{


    namespace PlayerFolder
    {
        public class PlayerAttack : MonoBehaviour
        {
            private InputManager _inputScript;
            [SerializeField]private Camera rayCastCamera;
            private Ray _ray;
            private RaycastHit _hit;
            [SerializeField] private float range;
            [SerializeField] private int damage;
            [SerializeField]private LayerMask enemyLayerMask;
            void Start()
            {
                _inputScript = InputManager.Instance;
                _inputScript.AttackEvent.AddListener(Attack);

            }

            // Update is called once per frame

            void Update()
            {

            }

            private void Attack()
            {
                Debug.Log("Attack");
                _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
                if (Physics.Raycast(_ray, out _hit,range,enemyLayerMask)){
                    DamageScript enemy = _hit.transform.GetComponent<DamageScript>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }

            //if (Mouse.current.leftButton.wasPressedThisFrame)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            //    RaycastHit hit;

            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        DamageScript enemy = hit.transform.GetComponent<DamageScript>();
            //        if (enemy != null)
            //        {
            //            enemy.TakeDamage(10);
            //        }
            //    }
            //}
        }
    }
}
