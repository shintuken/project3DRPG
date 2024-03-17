using RPG.Control;
using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{

    [SerializeField] private GameObject Character;
    [SerializeField] private GameObject Loading;
    [SerializeField] private Canvas UIGameOver;
    //Check other UI is display or note
    [SerializeField] private Canvas UIOtherScreen;

    //Get player controller
     private GameObject player;
     private PlayerController playerController;


    private Health playerHealth;
    private LoadingScreen loadingScreen;
    // Start is called before the first frame update
    void Start()
    {

        playerHealth = Character.GetComponent<Health>();
        if(playerHealth == null)
        {
            Debug.Log("Not get Player Health");
        }
        playerHealth.OnDeath += HandleDeath;
        UIGameOver.enabled = false;
        Loading.SetActive(false);
        loadingScreen = Loading.GetComponent<LoadingScreen>();

        //Get player controller to disable
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>(); 
    }

    private void HandleDeath()
    {
        //Nếu UI kia chưa xuất hiện thì mới hỉn thị
        if (!UIOtherScreen.isActiveAndEnabled)
        {
            // Gọi hàm hiển thị menu UI hoặc thực hiện các hành động khác khi nhân vật chết
            ShowDeathMenu();
        }
    }

    private void ShowDeathMenu()
    {
        // Hiển thị menu UI khi nhân vật chết
        UIGameOver.enabled = true;
        playerController.enabled = false;
    }

    public void ResetGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Loading.SetActive(true);
        loadingScreen.LoadScene(currentSceneIndex);



    }

    public void LoadMenuScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex - 1;

        // Ghi lại scene hiện tại vào PlayerPrefs
        PlayerPrefs.SetInt("PreviousSceneIndex", currentSceneIndex);

        Loading.SetActive(true);
        loadingScreen.LoadScene(nextSceneIndex);
    }
}


