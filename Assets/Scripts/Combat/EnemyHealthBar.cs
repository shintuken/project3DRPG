using RPG.Combat;
using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 1f;

    public Image frontHealthBar;
    public Image backHealthBar;

    private Fighter fighter;

    private void Awake()
    {
        fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
       
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        if (fighter != null)
        {
            Health health = fighter.GetTarget();
            UpdateHealthBarUI(health, 0);
        }

    }
    public void UpdateHealthBarUI(Health target, float lerpTimer)
    {
        this.lerpTimer = lerpTimer;

        //Đang quy đổi về hệ số 0-1 ra so sánh
        //Fill amount hiện tại cả thanh máu chính thức (front)
        float fillAmountFrontHealthBar = frontHealthBar.fillAmount;
        //Fill amount của máu tăng giảm 
        float fillAmountBackHealthBar = backHealthBar.fillAmount;
        //% HP hiện tại
        float healbarFraction = target.GetHealthFraction();

        //Trường hợp mất máu
        if (fillAmountBackHealthBar > healbarFraction)
        {
            frontHealthBar.fillAmount = healbarFraction;
            backHealthBar.color = Color.red;
            //Đếm thời gian giảm máu
            lerpTimer += Time.deltaTime;
            //thời gian giảm 
            float percentComplete = lerpTimer / chipSpeed;
            //Làm chuyển động mượt hơn
            //percentComplete = percentComplete * percentComplete;
            //chạy từ chỗ máu backhealthbar đến healthbarfraction trong thời gian percentComplete
            backHealthBar.fillAmount = Mathf.Lerp(fillAmountBackHealthBar, healbarFraction, percentComplete);
        }
        //Trường hợp tăng máu
        else if (fillAmountFrontHealthBar < healbarFraction)
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
