using UnityEngine;

public abstract class ItemButtonBase : MonoBehaviour
{
    public abstract void SetIndex(int index);
    public abstract void Set(ItemSlot slot);
    public abstract void Clean();
    public abstract void HighLight(bool b);
}
