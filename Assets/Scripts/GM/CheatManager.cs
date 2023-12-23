using RPG.Combat;
using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RPG.GM
{
    public class GMCode : MonoBehaviour
    {
        //List Button need to use
        [SerializeField] Button fullSpeedBtn;
        [SerializeField] Button fullHPBtn;
        [SerializeField] Button fullDamageBtn;
        [SerializeField] Button GoStartPointBtn;
        [SerializeField] Button GoEndPointBtn;
        [SerializeField] GameObject startPoint;
        [SerializeField] GameObject endPoint;

        [SerializeField] GameObject player;

        //List Bool to check GM on or off
        private bool onFullSpeed = false;
        private bool onFullHP = false;
        private bool onFullDamage = false;


        private PlayerController playerController;
        private Health playerHealth;
        private Weapon playerWeapon;

        private const float fullHealth = 9999999;
        private float normalHealth;
        private const float normalSpeedFraction = 1;
        private const float maxSpeedFraction = 30;
        private const float fullDamage = 999999;
        private float normalDamage;



        private void Awake()
        {
            playerController = player.GetComponent<PlayerController>();
            playerHealth = player.GetComponent<Health>();

        }

        //hack full speed
        public void FullSpeed()
        {
            if (!onFullSpeed)
            {
                playerController.UpdateSpeedFraction(maxSpeedFraction);
                fullSpeedBtn.GetComponentInChildren<TMP_Text>().text = "Full Speed: ON";
            }
            else
            {
                playerController.UpdateSpeedFraction(normalSpeedFraction);
                fullSpeedBtn.GetComponentInChildren<TMP_Text>().text = "Full Speed: OFF";
            }
            onFullSpeed = !onFullSpeed;
        }

        //hack full HP
        public void FullHP()
        {
            if (!onFullHP)
            {
                normalHealth = playerHealth.GetHealthPoints();
                playerHealth.SetHealth(fullHealth);
                fullHPBtn.GetComponentInChildren<TMP_Text>().text = "Full HP: ON";
            }
            else
            {
                playerHealth.SetHealth(normalHealth);
                fullHPBtn.GetComponentInChildren<TMP_Text>().text = "Full HP: OFF";
            }
            onFullHP = !onFullHP;
        }

        //hack full damage character
        public void FullDamage()
        {
            playerWeapon = player.GetComponent<Fighter>().GetWeapon();
            if (!onFullDamage)
            {
                normalDamage = playerWeapon.GetWeaponDamage();
                playerWeapon.SetWeaponDamage(fullDamage);
                fullDamageBtn.GetComponentInChildren<TMP_Text>().text = "Full Damage: ON";
            }
            else
            {
                playerWeapon.SetWeaponDamage(normalDamage);
                fullDamageBtn.GetComponentInChildren<TMP_Text>().text = "Full Damage: OFF";
            }
            onFullDamage = !onFullDamage;
        }

        //Go on spawn point in map
        public void GoStartPoint()
        {
            //Must turn off nav mesh to go through
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = startPoint.transform.GetChild(0).position;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        //Go to end Point in map
        public void GoEndPoint()
        {
            //Must turn off nav mesh to go through
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = endPoint.transform.GetChild(0).position;
            player.GetComponent<NavMeshAgent>().enabled = true;

        }

    }

}
