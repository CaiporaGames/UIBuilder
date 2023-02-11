using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Popup
{

    public class PopupSetup : MonoBehaviour
    {
        private void OnEnable()
        {
            VisualElement uidocument = GetComponent<UIDocument>().rootVisualElement;

            PopupWindow popupWindow = new PopupWindow();
            uidocument.Add(popupWindow);

            popupWindow.confirmed += () => Debug.Log("Email Confirmed!");
            popupWindow.canceled += () => Debug.Log("Email Canceld!");

            popupWindow.canceled += () => uidocument.Remove(popupWindow);
        }
    }
}
