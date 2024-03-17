using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{   
    [CreateAssetMenu(fileName ="Weapon", menuName = "Create new weapon", order = 0)]
    public class Weapon: ScriptableObject
    {
        //Prefab equipment
        [SerializeField] private GameObject equipedPrefab = null;
        //Animator equipment 
        [SerializeField] private AnimatorOverrideController overrideAnimator = null;
        //weapon stat
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float timeBetweenAttack = 1f;
        //Check hand size
        [SerializeField] private bool isRightHand = true;
        //Arrow, fire,etc
        [SerializeField] Projectile projectile = null;
        [SerializeField] bool isHoming = false;

        //for create weapon 
        private GameObject weaponInstance;

        public void Spawn(Transform rightHandWeapon, Transform leftHandWeapon, Animator animator)
        {
            //check if it unarmed or not, unarmed is default and set null variable
            if(equipedPrefab != null )
            {
                Transform handTransform;
                handTransform = GetHandTransform(rightHandWeapon, leftHandWeapon);
                weaponInstance = Instantiate(equipedPrefab, handTransform);
            }

            //check if it unarmed or not, unarmed is default and set null variable
            if (overrideAnimator != null)
            {
                animator.runtimeAnimatorController = overrideAnimator;
            }else if (overrideAnimator != null)
            { 
                animator.runtimeAnimatorController = overrideAnimator.runtimeAnimatorController;
            }
        }

        public void RemoveWeapon()
        {
          Destroy(weaponInstance);
        }

        private Transform GetHandTransform(Transform rightHandWeapon, Transform leftHandWeapon)
        {
            Transform handTransform;
            if (isRightHand)
            {
                handTransform = rightHandWeapon.transform;
            }
            else
            {
                handTransform = leftHandWeapon.transform;
            }
            return handTransform;
        }

        //Tạo projectile 
        public void LaunchProjectile(Transform rightHandWeapon, Transform leftHandWeapon, Health target)
        {
            //Phân biệt HandTransform ở tay trái hay tay phải để spawn ra
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHandWeapon, leftHandWeapon).position,Quaternion.identity);
            //set target để projecttile hướng đến
            projectileInstance.SetTarget(target, weaponDamage, isHoming);        
        }

        public bool HasProjectTile()
        {
            return projectile != null;
        }
        public float GetAttackRange()
        {
            return attackRange;
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }

        public void SetWeaponDamage(float damage)
        {
            weaponDamage = damage;
        }

        public float GetTimeBetweenAttack()
        {
            return timeBetweenAttack;
        }
    }
}