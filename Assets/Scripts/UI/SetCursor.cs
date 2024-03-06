using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    //Cursor
    public static Texture2D defaultCursor;
    public static Texture2D attackCursor;
    public static Texture2D CollectCursor;

    [SerializeField] private  Texture2D defaultCursorPrivate;
    [SerializeField] private Texture2D attackCursorPrivate;
    [SerializeField] private Texture2D CollectCursorPrivate;

    //singleton
    public static SetCursor instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one SetCursor in scene");
        }
        instance = this;
        defaultCursor = defaultCursorPrivate;
        attackCursor = attackCursorPrivate;
        CollectCursor = CollectCursorPrivate;
       
    }
    public void SetCursorTexture(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

}
