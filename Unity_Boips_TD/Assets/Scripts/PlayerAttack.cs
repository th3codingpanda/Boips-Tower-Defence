using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                DamageScript enemy = hit.transform.GetComponent<DamageScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(10);
                }
            }
        }
    }

    private void Attack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            DamageScript enemy = hit.transform.GetComponent<DamageScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
        }
    }


}
