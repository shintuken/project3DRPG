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

    //Cursor
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D attackCursor;
    
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
            Cursor.SetCursor(attackCursor, Vector2.zero, CursorMode.Auto);

            enemyOutline.enabled = true;
            enemyOutline.OutlineWidth = 2;
            enemyOutline.OutlineColor = Color.red;
        }
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        enemyOutline.enabled = false;
    }

    private void OnMouseDown()
    {
        
    }
}
