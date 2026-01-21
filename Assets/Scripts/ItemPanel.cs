using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<ItemButtonBase> buttons;

    bool _initialized;

    protected virtual void Start()
    {
        Init();
        _initialized = true;
    }

    protected virtual void OnEnable()
    {
        if (!_initialized) return;
        Refresh();
    }
    public void Init()
    {
        if (inventory == null || inventory.slots == null || buttons == null)
        {
            Debug.LogError($"{name}: Missing inventory/slots/buttons.");
            return;
        }

        SetIndex();
        Show();
    }

    //public void Init()
    //{
    //    if (inventory == null || inventory.slots == null || buttons == null)
    //    {
    //        Debug.LogError($"{name}: Missing inventory/slots/buttons.");
    //        return;
    //    }

    //    int n = Mathf.Min(inventory.slots.Count, buttons.Count);
    //    for (int i = 0; i < n; i++)
    //        if (buttons[i] != null) buttons[i].SetIndex(i);

    //    SetIndex();
    //    Show();
    //}

    void SetIndex()
    {
        int n = Mathf.Min(inventory.slots.Count, buttons.Count);
        for (int i = 0; i < n; i++)
        {
            if (buttons[i] == null)
            {
                Debug.LogWarning($"{name}: buttons[{i}] is NULL");
                continue;
            }

            buttons[i].SetIndex(i);
            Debug.Log($"{name}: SetIndex {buttons[i].name} => {i}");
        }

    }


    public void Show()
    {
        if (inventory == null || inventory.slots == null || buttons == null) return;

        int n = Mathf.Min(inventory.slots.Count, buttons.Count);
        for (int i = 0; i < n; i++)
        {
            var btn = buttons[i];
            if (btn == null) continue;

            var slot = inventory.slots[i];
            if (slot == null || slot.item == null) btn.Clean();
            else btn.Set(slot);
        }
    }

    public void Refresh() => Show();
    public virtual void OnClick(int id) { }
}
