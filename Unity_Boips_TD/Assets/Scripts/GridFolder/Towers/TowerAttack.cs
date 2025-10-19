using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerAttack : MonoBehaviour
{
    //[SerializeField] private float attackRange = 5f;
    //[SerializeField] private float attackDamage = 10f;
    //[SerializeField] private float attackRate = 1f;
    //[SerializeField] private TagField _enemyTag;
    //[SerializeField] private Transform firePoint;

    [SerializeField] private float _attackSpeed;
    private float _attackPerSecond;


    private float _distance;
    public float Radius = 0f;
    private Enemy _closestEnemy;
    Enemy target;

    [SerializeField] private ParticleSystem _towerBullet;

    public bool IsSpeedingUp => Time.time < _attackPerSecond; 
    
    
   

    void Start()
    {
      
    }

    
    void Update()
    {
       
        
            TowerShoot();
           
        
        //FindClosestEnemy();
    }

    private void FindClosestEnemy()
    {
        //int Colliders = Physics.OverlapSphereNonAlloc(transform.position, Radius, new Collider[10]);

        int MaxColliders = 20;
        Collider[] hitColliders = new Collider[MaxColliders];

        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, Radius, hitColliders);
        float closestDistanceSqr = Mathf.Infinity;

        for (int i = 0; i < numColliders; i++) 
        {
            //EnemyTarget target;
            //hitColliders[i].TryGetComponent<EnemyTarget>(out target);
            hitColliders[i].TryGetComponent(out target);

            if (target != null) 
            {
                //float _distanceToTarget = (hitColliders[i].transform.position - transform.position).sqrMagnitude;
                float _distanceToTarget = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                if (_distanceToTarget < closestDistanceSqr) 
                {
                    if (_closestEnemy != null)
                    {
                        //_closestEnemy.UnTarget();
                    }

                    _closestEnemy = target;
                    closestDistanceSqr = _distanceToTarget;
                    //_closestEnemy.Target();
                    Debug.Log($"Target Acquired: {_closestEnemy}");

                }
            }
        }
    }

    private void TowerShoot()
    {
        FindClosestEnemy();

       
        if (_closestEnemy != null)
        {
            _distance = Vector3.Distance(_closestEnemy.transform.position, this.gameObject.transform.position);
            Debug.Log($"Distance to target: {_distance}");
            if (_distance > 0 || _distance <= Radius && _closestEnemy != null)
            {
                if (IsSpeedingUp) return;
                Debug.Log("Hit an enemy!");
                _closestEnemy.transform.GetComponent<DamageScript>().TakeDamage(10);
                AttackSpeed();
                ParticleEffect();
              
            }
            //if (_distance > Radius || _closestEnemy == null)
            //{
            //    Debug.Log("No target in radius");
            //}
        }

        else if( _closestEnemy == null)
        {
            Debug.Log("No target in radius");
        }

        //_distance > Radius ||

        //if (_distance <= 30f && _closestEnemy != null)
        //{
        //    if (isCoolingDown) return;
        //    Debug.Log("Hit an enemy!");
        //    _closestEnemy.transform.GetComponent<DamageScript>().TakeDamage(10);
        //    CoolDownStart();

        //}

    }

    private void AttackSpeed() 
    {         
        _attackPerSecond = Time.time + (1f / _attackSpeed);
    }

    private void ParticleEffect()
    {
        //if (_attackPerSecond > 0) {
        _towerBullet.Clear();
        _towerBullet.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
    
}
