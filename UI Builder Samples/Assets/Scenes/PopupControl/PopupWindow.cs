using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Popup
{
    public class PopupWindow : VisualElement
    {

        public event Action confirmed;
        public event Action canceled;

        [UnityEngine.Scripting.Preserve]
        public new class UxmlFactory : UxmlFactory<PopupWindow> { }

        const string popupWindowStylesheet = "PopupStylesheet";
        const string popWindowStyle = "popWindow";
        const string popupContainerStyle = "popupContainer";
        const string horizontalContainerStyle = "horizontalContainer";
        const string messageLabelStyle = "messageLabel";
        const string popupButtonStyle = "popupButton";
        const string buttonConfirmStyle = "buttonConfirm";
        const string buttonCancelStyle = "buttonCancel";

        public PopupWindow()
        {
            styleSheets.Add(Resources.Load<StyleSheet>(popupWindowStylesheet));

            AddToClassList(popupContainerStyle);

            VisualElement window = new VisualElement();
            window.AddToClassList(popWindowStyle);
            hierarchy.Add(window);

            VisualElement horizontalContainerText = new VisualElement();
            horizontalContainerText.AddToClassList(horizontalContainerStyle);
            window.Add(horizontalContainerText);

            Label messageLabel = new Label();
            messageLabel.text = "Random Text";
            messageLabel.AddToClassList(messageLabelStyle);
            horizontalContainerText.Add(messageLabel);

            VisualElement horizontalContainerButton = new VisualElement();
            horizontalContainerButton.AddToClassList(horizontalContainerStyle);
            window.Add(horizontalContainerButton);

            Button confirmButton = new Button() { text = "SUCCESS" };
            confirmButton.AddToClassList(popupButtonStyle);
            confirmButton.AddToClassList(buttonConfirmStyle);
            horizontalContainerButton.Add(confirmButton);

            Button cancelButton = new Button() { text = "CANCEL" };
            cancelButton.AddToClassList(popupButtonStyle);
            cancelButton.AddToClassList(buttonCancelStyle);
            horizontalContainerButton.Add(cancelButton);

            confirmButton.clicked += OnConfirm;
            cancelButton.clicked += OnCancel;
        }

        void OnConfirm()
        {
            confirmed?.Invoke();
        }

        void OnCancel()
        {
            canceled?.Invoke();
        }
    }
}
