using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    public static HighlightObject instance {  get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one HighlightObject in scene");
        }
       instance = this;

    }
    public void Highlight(GameObject obj, Outline outlineObj)
    {
       //Create Outline for object
       Outline outline = obj.GetComponent<Outline>();
        //Set objec outline
        outline.enabled = true;
        outline = outlineObj;
    }
}
