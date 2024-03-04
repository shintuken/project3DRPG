using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HighlightEnemey : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private Outline enemyOutline;
    private Health enemyHeath;
    private bool enemyIsDead = false;

    private void Start()
    {
        enemyOutline = enemy.GetComponent<Outline>();
        enemyOutline.enabled = false;
        enemyHeath = enemy.GetComponent<Health>();
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
            enemyOutline.enabled = true;
            enemyOutline.OutlineWidth = 2;
            enemyOutline.OutlineColor = Color.red;
        }
    }
    private void OnMouseExit()
    {
        enemyOutline.enabled = false;
    }

    private void OnMouseDown()
    {
        
    }
}
