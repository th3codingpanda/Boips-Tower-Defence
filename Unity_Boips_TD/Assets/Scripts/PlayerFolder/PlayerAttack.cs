using EnemyFolder;
using UnityEngine;
using UnityEngine.InputSystem;

<<<<<<< Updated upstream
namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private InputManager input;
        void Start()
        {
            input = InputManager.Instance;
            input.AttackEvent.AddListener(Attack);
=======
namespace PlayerFolder
{
    public class PlayerAttack : MonoBehaviour
    {
        private InputManager _inputScript;
        void Start()
        {
            _inputScript = InputManager.Instance;
            _inputScript.AttackEvent.AddListener(Attack);
>>>>>>> Stashed changes
        }

        // Update is called once per frame

        void Update()
        {
       
        }

        private void Attack()
        {
            Debug.Log("Attack");
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                DamageScript enemy = hit.transform.GetComponent<DamageScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(25);

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
