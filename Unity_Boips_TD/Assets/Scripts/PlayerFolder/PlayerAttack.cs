using System.Collections.Generic;
using EnemyFolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerFolder
{


    namespace PlayerFolder
    {
        public class PlayerAttack : MonoBehaviour
        {
            private InputActionManager inputActionScript;
            [SerializeField]private Camera rayCastCamera;
            private Ray _ray;
            private RaycastHit _hit;
            [SerializeField] private float range;
            [SerializeField] private int damage;
            [SerializeField]private LayerMask enemyLayerMask;
            [SerializeField]List<AudioSource> attackAudios;
            [SerializeField] private Camera playerCamera;
            [SerializeField] private ParticleSystem _wandParticles; 
            [SerializeField] private Transform wandTip;

            void Start()
            {
                inputActionScript = InputActionManager.Instance;
                inputActionScript.AttackEvent.AddListener(Attack);

            }

            // Update is called once per frame

            void Update()
            {

            }

            private void Attack()
            {
                WandParticles();
                int random = Random.Range(0, 2);
                attackAudios[random].pitch = Random.Range(0.8f, 1.2f);
                attackAudios[random].Play();
                _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
                if (Physics.Raycast(_ray, out _hit,range,enemyLayerMask)){
                    DamageScript enemy = _hit.transform.GetComponent<DamageScript>();
                    enemy.TakeDamage(damage);
                }
            }

            private void WandParticles()
            {
                _wandParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                ParticleSystem.MainModule main = _wandParticles.main;

                float particlesSpeed = main.startSpeed.constant;
                main.startLifetime = range / particlesSpeed;
                //_wandParticles.Clear();
                _wandParticles.Play();
            }
        }
    }
}
