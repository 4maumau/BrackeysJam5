using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class MouseClickEvent : UnityEvent<Vector2>
{
}

public class MousePosition : MonoBehaviour
{
    public MouseClickEvent OnMouseClick;
    
    public Vector2 mouseWorldPosition;

    void Start()
    {
        if (OnMouseClick == null)
            OnMouseClick = new MouseClickEvent();

        OnMouseClick.AddListener(Ping);
    }


    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick?.Invoke(mouseWorldPosition);
        }
        
    }

    void Ping(Vector2 pos)
    {
        //print("mouse position: " + pos);
    }
}
