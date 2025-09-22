using System;
using UnityEngine;

public class DamageScript : MonoBehaviour, IEnemyTarget
{

    [SerializeField] private int _hp = 100;
    private Renderer _render;
    private Color _defColor;

    public void ColorChange()
    {
        _render.material.color = Color.red;
    }

    void Start()
    {
        _render = GetComponent<Renderer>();
        _defColor = _render.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //TakeDamage(1);
    }

    void IEnemyTarget.Target()
    {
        //ColorChange();
    }
    void IEnemyTarget.UnTarget()
    {
        //_render.material.color = _defColor;
    }
    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if(_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

   
}
