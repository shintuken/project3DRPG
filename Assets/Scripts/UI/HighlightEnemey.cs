using JetBrains.Annotations;
using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HighlightEnemey : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    //Outline Enemy 
    private Outline enemyOutline;
    //Enemy Health
    private Health enemyHeath;

    
    private bool enemyIsDead = false;

    private void Start()
    {
        enemyOutline = enemy.GetComponent<Outline>();
        enemyOutline.enabled = false;
        enemyHeath = enemy.GetComponent<Health>();
        //Event check enemy death or not
        enemyHeath.OnDeath += Handle_EnemyHealth;
    }

    private void Handle_EnemyHealth()
    {
        enemyIsDead = true;
    }

    void OnMouseOver()
    {
        if (!enemyIsDead)
        {
            SetCursor.instance.SetCursorTexture(SetCursor.attackCursor);

            enemyOutline.enabled = true;
            enemyOutline.OutlineWidth = 2;
            enemyOutline.OutlineColor = Color.red;
            //Set outline
            HighlightObject.instance.Highlight(enemy,enemyOutline);
        }
    }
    private void OnMouseExit()
    {
        SetCursor.instance.SetCursorTexture(SetCursor.defaultCursor);
        enemyOutline.enabled = false;
    }

}
