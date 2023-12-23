using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCheat : MonoBehaviour
{
    //List button in GM 
    [SerializeField] GameObject listGM;
    //Button to open GM
    [SerializeField] GameObject buttonGM;
    //Hde GM Button
    [SerializeField] GameObject buttonExit;
    private bool displayGM = true;
    
    //Display GM
    public void DisplayGM()
    {
        if (listGM == null || buttonGM == null)
        {
            Debug.Log("List GM or button GM null");
            return;
        }
        listGM.SetActive(true);
        buttonGM.SetActive(false);
    }

    //Hide GM
    public void ExitGM()
    {
        if (listGM == null || buttonGM == null)
        {
            Debug.Log("List GM or button GM null");
            return;
        }
        listGM.SetActive(false);
        buttonGM.SetActive(true);
    }

    //Press F1 to open or Hide GM
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            buttonGM.SetActive(displayGM);
            displayGM = !displayGM;
        }
    }
}
