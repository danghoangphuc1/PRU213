using UnityEngine;
using UnityEngine.EventSystems;

public class ToolbarSlotTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int slotIndex; // index trong inventory toolbar

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = GameManager.instance.inventoryContainer.slots[slotIndex].item;
        if (item != null) TooltipUI.Instance.Show(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance.Hide();
    }
}
