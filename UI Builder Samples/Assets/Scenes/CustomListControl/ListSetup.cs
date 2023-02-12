using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ListSetup : MonoBehaviour
{
    ListWindow listWindow;
    private void OnEnable()
    {
        VisualElement uidocument = GetComponent<UIDocument>().rootVisualElement;

        listWindow = new ListWindow();
        uidocument.Add(listWindow);


        //popupWindow.confirmed += () => Debug.Log("Email Confirmed!");
        //popupWindow.canceled += () => Debug.Log("Email Canceld!");

        //popupWindow.canceled += () => uidocument.Remove(popupWindow);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            listWindow.CreateCard();
        }
    }
}

