using UnityEngine;
using UnityEngine.UIElements;

public class HealthbarUI : MonoBehaviour
{
    /*  The world space position that the UI should render at. Since the boat can move, you will need a constant reference to follow along with. This will be set through the inspector later. */
   public Transform TransformToFollow;

    private VisualElement m_Bar;
    public Camera m_MainCamera;

    private void Start()
    {
        m_Bar = GetComponent<UIDocument>().rootVisualElement.Q("container");
        m_Bar.visible = true;
        SetPosition();
    }

    public void SetPosition()
    {
        Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel(
            m_Bar.panel, TransformToFollow.position, m_MainCamera);

            m_Bar.transform.position = newPosition.WithNewX(newPosition.x - 
            m_Bar.layout.width / 2);

    }

    private void LateUpdate()
{
    if (TransformToFollow != null)
    {
        SetPosition();
    }
}
}
