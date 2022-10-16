using UnityEngine;
using UnityEngine.UIElements;// STEP 0 -> UI library
public class ClickButton : MonoBehaviour
{
    // STEP 1 -> Internal cached reference to the UXML elements we want to access
    VisualElement box;
    Button button;

    // STEP 2 -> Initialize the necessary connections and functionalities
    private void OnEnable() {

        // STEP 3 -> Get access to the UIDocument
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;

        // STEP 4 -> Bind the UXML elements with the internal reference form STEP 1.
        box = root.Q("Box");//OBS.: String name MUST be the same as the given on the UXML element
        button = root.Q<Button>("ClickButton");//OBS.: We need to tell the type element we want. Here is Button. In the above example we do not need because the default is visual element.

        // STEP 5 -> Creating the functionality when the button is clicked.
        button.clicked += OnButtonClicked;//OBS.: We use delegates to connect to the functionality

    }


    // STEP 5 -> Create a method to do some functionality.
    void OnButtonClicked()
    {
        float randomWidth = Random.Range(50, 300);
        box.style.width = randomWidth;
        ChangeColor();
    }

     void ChangeColor()
    {
        Debug.Log("COLOR");
        float randomWidth = Random.value;
        box.style.backgroundColor = new Color(randomWidth, randomWidth*0.5f, randomWidth*0.3f, 1);
    }
}
