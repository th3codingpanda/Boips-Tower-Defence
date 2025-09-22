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

    [SerializeField] private float _attackCooldown;
    private float _cooldownTimer;
    [SerializeField] private float _distance;
    [SerializeField] private GameObject enemyPrefab;

    public float Radius = 15f;
    private EnemyTarget _closestEnemy;
    EnemyTarget target;


    //[SerializeField] private GameObject[] AllEnemyObjects;


    public bool isCoolingDown => Time.time < _cooldownTimer;
    
    //enemyPrefab = compareTag("enemy");
    //if (.CompareTag("enemy"))
    //public List<GameObject> enemiesInRange;

    void Start()
    {
        //enemiesInRange = new List<GameObject>(Resources.LoadAll<GameObject>("enemy"));

        //AllEnemyObjects = GameObject.;
    }

    // Update is called once per frame
    void Update()
    {
        TowerShoot();
        FindClosestEnemy();
    }

    private void FindClosestEnemy()
    {
        //int Colliders = Physics.OverlapSphereNonAlloc(transform.position, Radius, new Collider[10]);
        int Colliders = 5;
        Collider[] hitColliders = new Collider[Colliders];
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
                        _closestEnemy.UnTarget();
                    }

                    _closestEnemy = target;
                    closestDistanceSqr = _distanceToTarget;
                    _closestEnemy.Target();
                    Debug.Log($"Target Acquired: {_closestEnemy}");

                }
            }
        }
    }

    private void TowerShoot()
    {
        FindClosestEnemy();
        //target = _closestEnemy
        _distance = Vector2.Distance(target.transform.position, this.gameObject.transform.position);

        if (_distance <= 10f && enemyPrefab != null)
        {
            if (isCoolingDown) return;
            Debug.Log("Hit an enemy!");
            enemyPrefab.transform.GetComponent<DamageScript>().TakeDamage(10);
            CoolDownStart();

        }
       
    }
    
    private void CoolDownStart() 
    {         _cooldownTimer = Time.time + _attackCooldown;
    }
}
