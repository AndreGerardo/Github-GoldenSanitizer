using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public UnityEvent onDown;
    public UnityEvent onUp;


    public void OnPointerDown(PointerEventData eventData)
    {
        onDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onUp.Invoke();
    }
    

}
