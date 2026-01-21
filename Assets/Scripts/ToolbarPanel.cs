using UnityEngine;

public class ToolbarPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.toolbarController.Set(id);
        GameManager.instance.dragAndDropController.Onclick(inventory.slots[id]);
        Show();
    }
}
