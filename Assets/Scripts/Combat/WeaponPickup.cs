using FMODUnity;
using RPG.Combat;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5f;





        private void OnTriggerEnter(Collider other)
        {

            if(other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.pickUpWeapon, this.transform.position);
                StartCoroutine(WaitToRespawn(respawnTime));
            }

        }
        IEnumerator WaitToRespawn (float respawnTime)
        {
            ShowPickUpItem(false);
            yield return new WaitForSeconds(respawnTime);
            ShowPickUpItem(true);
        }

        private void ShowPickUpItem(bool shouldShow)
        {
            gameObject.GetComponent<Collider>().enabled = shouldShow;
            int childLength = transform.childCount;
            for (int i = 0; i < childLength; i++)
            {
                transform.GetChild(i).gameObject.SetActive(shouldShow);
            }
        }
    }
}

