using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    public Texture2D cursorGorunum;

    Vector2 cursorPosition;
    // Start is called before the first frame update
    void Start()
    {
        cursorPosition=new Vector2(cursorGorunum.width/4,cursorGorunum.height/2);
        Cursor.SetCursor(cursorGorunum, cursorPosition, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
