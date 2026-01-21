using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ToolbarButton : ItemButtonBase, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image highlight;

    int myIndex;

    public override void SetIndex(int index)
    {
        myIndex = index;
    }

    public override void Set(ItemSlot slot)
    {
        if (slot == null || slot.item == null)
        {
            Clean();
            return;
        }

        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;

        if (slot.item.stackable)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    public override void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);

        if (highlight != null)
            highlight.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //var hit = eventData.pointerCurrentRaycast.gameObject;
        //Debug.Log($"Hit={hit.name} | Handler={name} | slot={myIndex}");
        //GetComponentInParent<ItemToolbarPanel>()?.OnClick(myIndex);


        //if (eventData.button != PointerEventData.InputButton.Left) return;

        //var panel = GetComponentInParent<ItemToolbarPanel>();
        //if (panel == null)
        //{
        //    Debug.LogError($"{name}: Không tìm thấy ItemPanel/ItemToolbarPanel ở parent.");
        //    return;
        //}

        //panel.OnClick(myIndex);
        //Debug.Log($"Click slot {myIndex} -> panel = {panel.name}");

        if (eventData.button != PointerEventData.InputButton.Left) return;

        int index = transform.GetSiblingIndex(); // index theo thứ tự trong Hierarchy
        var panel = GetComponentInParent<ItemToolbarPanel>();
        panel?.OnClick(index);

    }

    public override void HighLight(bool b)
    {
        if (highlight == null)
        {
            Debug.LogError($"{name}: highlight is NULL (chưa kéo Image highlight vào Inspector)");
            return;
        }
        highlight.gameObject.SetActive(b);
        //if (highlight != null)
        //    highlight.gameObject.SetActive(b);
    }
}
