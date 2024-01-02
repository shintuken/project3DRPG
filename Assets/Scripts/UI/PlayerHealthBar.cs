using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        private float health;
        private float lerpTimer;
        public float maxHealth = 100;
        public float chipSpeed = 1f;

        public Image frontHealthBar;
        public Image backHealthBar;

        public void UpdateHealthBarUI(float healthPoint, float maxHealthPoint, float lerpTimer)
        {
            this.lerpTimer = lerpTimer;
            health = healthPoint;
            maxHealth = maxHealthPoint;

            //Đang quy đổi về hệ số 0-1 ra so sánh
            //Fill amount hiện tại cả thanh máu chính thức (front)
            float fillAmountFrontHealthBar = frontHealthBar.fillAmount;
            //Fill amount của máu tăng giảm 
            float fillAmountBackHealthBar = backHealthBar.fillAmount;
            //% HP hiện tại
            float healbarFraction = health / maxHealth;

            //Trường hợp mất máu
            if (fillAmountBackHealthBar > healbarFraction)
            {
                frontHealthBar.fillAmount = healbarFraction;
                backHealthBar.color = Color.red;
                //Đếm thời gian giảm máu
                lerpTimer += Time.deltaTime;
                //thời gian giảm 
                float percentComplete = lerpTimer / chipSpeed;
/*                //Làm chuyển động mượt hơn
                percentComplete = percentComplete * percentComplete;*/
                //chạy từ chỗ máu backhealthbar đến healthbarfraction trong thời gian percentComplete
                backHealthBar.fillAmount = Mathf.Lerp(fillAmountBackHealthBar, healbarFraction, percentComplete);
            }
            //Trường hợp tăng máu
            if (fillAmountFrontHealthBar < healbarFraction)
            {
                //Set thanh máu động sang màu xanh
                backHealthBar.color = Color.green;
                //Set lại thanh máu xanh tăng lên trước
                backHealthBar.fillAmount = healbarFraction;
                //thời gian máu giảm đi
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / chipSpeed;
                //Làm chuyển động mượt hơn
                //percentComplete = percentComplete * percentComplete;
                //Set thanh máu chính di chuyển tăng lên = với thanh máu động
                frontHealthBar.fillAmount = Mathf.Lerp(fillAmountFrontHealthBar, backHealthBar.fillAmount, percentComplete);
            }

        }

        void RestoreHealth(float healthRestore)
        {
            health += healthRestore;
            lerpTimer = 0;
        }
    }
}


