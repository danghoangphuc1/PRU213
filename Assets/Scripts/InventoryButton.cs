using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryButton : ItemButtonBase, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image highlight;

    int myIndex;

    public override void SetIndex(int index) => myIndex = index;

    public override void Set(ItemSlot slot)
    {
        if (slot == null || slot.item == null) { Clean(); return; }

        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;

        if (slot.item.stackable)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else text.gameObject.SetActive(false);
    }

    public override void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        if (highlight != null) highlight.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        var panel = GetComponentInParent<ItemPanel>();
        if (panel == null) return;

        panel.OnClick(myIndex);
    }

    public override void HighLight(bool b)
    {
        if (highlight != null) highlight.gameObject.SetActive(b);
    }
}
