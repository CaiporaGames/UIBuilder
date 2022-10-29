using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    static VisualElement ghostIcon;
    private static bool m_IsDragging;
    private static InventorySlot m_OriginalSlot;
    VisualElement root;
    VisualElement slotContainer;

    // Start is called before the first frame update
     private void Awake() 
    {
        //store the root from the UI document component
        root = GetComponent<UIDocument>().rootVisualElement;

        //search the root for slotContainer visualElement
        slotContainer = root.Q<VisualElement>("slotContainer");

        //get ghost icon
        ghostIcon = root.Q<VisualElement>("ghostIcon");

        //Create inventoryslots and add them as children of the slotcontainer
        for(int i = 0; i < 4; i++)
        {
            InventorySlot item = new InventorySlot();
            inventorySlots.Add(item);
            slotContainer.Add(item);
        }
        GameController.OnInventoryChanged += GameController_OnInventoryChanged;
        ghostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        ghostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    public static void StartDrag(Vector2 position, InventorySlot originalSlot)
    {
        //Set tracking variables
        m_IsDragging = true;
        m_OriginalSlot = originalSlot;
        //Set the new position
        ghostIcon.style.top = position.y - ghostIcon.layout.height * 0.5f;
        ghostIcon.style.left = position.x - ghostIcon.layout.width * 0.5f;
        //Set the image
        ghostIcon.style.backgroundImage = GameController.GetItemByGuid(originalSlot.iconName).Icon.texture;
        //Flip the visibility
        ghostIcon.style.visibility = Visibility.Visible;
    }

    private void GameController_OnInventoryChanged(string[] itemGuid, InventoryChangeType change)
    {
        //Loop through each item and if it has been picked up, add it to the next empty slot
        foreach (string item in itemGuid)
        {
            if (change == InventoryChangeType.Pickup)
            {
                var emptySlot = inventorySlots.FirstOrDefault(x => x.iconName.Equals(""));
                            
                if (emptySlot != null)
                {
                    emptySlot.HoldItem(GameController.GetItemByGuid(item));
                }
            }
        }
    }


    private void OnPointerMove(PointerMoveEvent evt)
{
    //Only take action if the player is dragging an item around the screen
    if (!m_IsDragging)
    {
        return;
    }

    //Set the new position
    ghostIcon.style.top = evt.position.y - ghostIcon.layout.height / 2;
    ghostIcon.style.left = evt.position.x - ghostIcon.layout.width / 2;

}

private void OnPointerUp(PointerUpEvent evt)
{
    if (!m_IsDragging)
    {
        return;
    }

    //Check to see if they are dropping the ghost icon over any inventory slots.
    IEnumerable<InventorySlot> slots = inventorySlots.Where(x => 
           x.worldBound.Overlaps(ghostIcon.worldBound));

    //Found at least one
    if (slots.Count() != 0)
    {
        InventorySlot closestSlot = slots.OrderBy(x => Vector2.Distance
           (x.worldBound.position, ghostIcon.worldBound.position)).First();
        
        //Set the new inventory slot with the data
        closestSlot.HoldItem(GameController.GetItemByGuid(m_OriginalSlot.iconName));
        
        //Clear the original slot
        m_OriginalSlot.DropItem();
    }
    //Didn't find any (dragged off the window)
    else
    {
        m_OriginalSlot.iconImage.image = 
              GameController.GetItemByGuid(m_OriginalSlot.iconName).Icon.texture;
    }

    //Clear dragging related visuals and data
    m_IsDragging = false;
    m_OriginalSlot = null;
    ghostIcon.style.visibility = Visibility.Hidden;

}
}
