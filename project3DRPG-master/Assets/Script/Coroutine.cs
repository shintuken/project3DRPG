using System.Collections;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    Material material;
    private void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }

    IEnumerator Blink()
    {
        Color materialColor = material.color;
        while (true)
        {
            //Color colorOri = new Color();
            //colorOri = material.color;
            gameObject.SetActive(false);
            //material.color = new Color(255f, colorOri.g, colorOri.b, 0f);
            //material.color = Color.black;
            //materialColor = Color.black;
            yield return new WaitForSeconds(.2f);
            gameObject.SetActive(true);
            //material.color = Color.white;
            //materialColor = Color.white;
            //material.color = new Color(3f, colorOri.g, colorOri.b, 0f);
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator RedDot()
    {
        while (true)
        {
            Color colorOri = material.color;
            material.color = new Color(255f, colorOri.g, colorOri.b, 0f);
            yield return new WaitForSeconds(.2f);
            material.color = new Color(3f, colorOri.g, colorOri.b, 0f);
            yield return new WaitForSeconds(.2f);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            StartCoroutine(Blink());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(RedDot());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StopCoroutine(Blink());
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            StopCoroutine(RedDot());
        }
        if(Input.GetKeyDown (KeyCode.Space)) 
        { 
            StopAllCoroutines();
        }

        
    }
}
