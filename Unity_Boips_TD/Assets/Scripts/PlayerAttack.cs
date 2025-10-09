using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private InputPackageManagerScript _inputScript;
    void Start()
    {
        _inputScript = InputPackageManagerScript.Instance;
        _inputScript.AttackEvent.AddListener(Attack);
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
