using UnityEngine;

public class ItemToolbarPanel : ItemPanel
{
    [SerializeField] ToolbarController toolbarController;
    int currentSelectedTool = -1;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (toolbarController == null)
            toolbarController = GameManager.instance.toolbarController; // đảm bảo đúng controller đang nhận scroll

        if (toolbarController == null) return;

        // chống subscribe trùng
        toolbarController.onChange -= Highlight;
        toolbarController.onChange += Highlight;

        // set mặc định qua controller (để đồng bộ với event)
        toolbarController.Set(0);
    }

    void OnDisable()
    {
        if (toolbarController != null)
            toolbarController.onChange -= Highlight;
    }

    public override void OnClick(int id)
    {
        toolbarController?.Set(id); // Set() phải invoke onChange
    }

    public void Highlight(int id)
    {
        if (buttons == null || buttons.Count == 0) return;

        id = Mathf.Clamp(id, 0, buttons.Count - 1);

        if (currentSelectedTool >= 0 &&
            currentSelectedTool < buttons.Count &&
            buttons[currentSelectedTool] != null)
        {
            buttons[currentSelectedTool].HighLight(false);
        }

        currentSelectedTool = id;

        if (buttons[currentSelectedTool] != null)
            buttons[currentSelectedTool].HighLight(true);
    }
}
