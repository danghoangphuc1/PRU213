using Unity.VisualScripting;
using UnityEngine;

public class ItemToolbarPanel : ItemPanel
{
    [SerializeField] ToolbarController toolbarController;

    private void Start()
    {
        Init();
        toolbarController.onChange += Highlight;
        Highlight(0);
    }
    public override void OnClick(int id)
    {
        toolbarController.Set(id);
        Highlight(id);
    }

    int currentSelectedTool;

    public void Highlight(int id)
    {
        buttons[currentSelectedTool].HighLight(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].HighLight(true);

    }
}
