using RPG.Core;
using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect = null;
        [SerializeField] private float projectileSpeed = 0.1f;
        //life time of a projecttile
        [SerializeField] private float maxLifeTime = 3f;
        [SerializeField] GameObject[] destroyOnHit = null;  
        [SerializeField] private float lifeAfterImpact = 2.5f;


        private GameObject blowEffectInstance;
        bool isHoming = false;
        Health target = null;
        float damage = 0;

        private void Start()
        {
            if(target != null)
            {
                transform.LookAt(GetAimPosition());
            }
        }


        void Update()
        {
            if (target == null) return;
            //Nhìn về hướng target
            if (isHoming && !target.IsDeath()) 
            {
                transform.LookAt(GetAimPosition());
                //Vector 3 fwd tức là toạ độ 0,0,1
                //Lấy toạ độ đó * speed vs delta Time để update thời gian  
            }
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

        }

        //Set mục tiêu bắn
        public void SetTarget(Health target, float damage, bool isHoming)
        {
            this.target = target;
            this.damage = damage;
            this.isHoming = isHoming;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimPosition()
        {
            CapsuleCollider playerCapsule = target.GetComponent<CapsuleCollider>();
            //Vector3.up là di chuyển theo phương y (0,1,0) 
            //Ở đây chúng ta chỉ đang thay đổi theo chiều dọc nên chỉ cần dùng Vector3.up
            if (playerCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * playerCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Health>() == target)
            {
                if (target.IsDeath()) return;
                if(hitEffect != null)
                {
                    blowEffectInstance = Instantiate(hitEffect, GetAimPosition(), target.transform.rotation);
                }
                target.TakeDamage(damage);

                projectileSpeed = 0;

                foreach (GameObject destroyItem in destroyOnHit)
                {
                    Destroy(destroyItem);
                }
                Destroy(gameObject, lifeAfterImpact);
            }
        }
    }
}

