using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class ListWindow : VisualElement
{

    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<ListWindow> { }

    public event Action createCard;

    const string listWindowStylesheet = "CustomListStylesheet";
    const string listWindowStyle = "listWindow";
    const string cardContainerStyle = "cardContainer";

    VisualElement window;
    public ListWindow()
    {
        styleSheets.Add(Resources.Load<StyleSheet>(listWindowStylesheet));

        window = new VisualElement();
        window.AddToClassList(listWindowStyle);
        hierarchy.Add(window);
        CreateCard();
    }

   public void CreateCard()
    {
        VisualElement cardContainer = new VisualElement();
        cardContainer.AddToClassList(cardContainerStyle);
        window.Add(cardContainer);
    }

}
