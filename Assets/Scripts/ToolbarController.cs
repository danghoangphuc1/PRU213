using System;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12;
    int selectedTool;

    public Action<int> onChange;

    public Item GetItem
    {
        get 
        {
            return GameManager.instance.toolbarContainer.slots[selectedTool].item;
        }
    }


    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            if (delta > 0)
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            else
            {
                selectedTool -= 1;
                selectedTool = (selectedTool <= 0 ? toolbarSize - 1 : selectedTool);

            }
            onChange?.Invoke(selectedTool);
        }
    }
    //chat
    public void Set(int id)
    {
        selectedTool = Mathf.Clamp(id, 0, toolbarSize - 1);
        onChange?.Invoke(selectedTool); // để click cũng update highlight giống scroll
    }


    //internal void Set(int id)
    //{
    //    selectedTool = id;
    //}

}
