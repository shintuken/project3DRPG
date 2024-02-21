using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button btnNewGame;
    [SerializeField] private Button btnOption;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnExit;

    private void Start()
    {
        btnNewGame.onClick.AddListener(MenuNewGame);
        btnOption.onClick.AddListener(MenuOption);
        btnContinue.onClick.AddListener(MenuContinue);
        btnExit.onClick.AddListener(MenuExit);
    }

    private void MenuExit()
    {
        Application.Quit();
    }

    private void MenuContinue()
    {
        Debug.Log("Button Continue Clicked");
    }

    private void MenuOption()
    {
        Debug.Log("Option Clicked");

    }

    private void MenuNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
