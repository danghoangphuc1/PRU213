using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;

    public ItemContainer inventoryContainer;

    public ToolbarController toolbarController; //phuc
    public ItemContainer toolbarContainer;  //phuc

    public DialogueSystem dialogueSystem;

    public ItemDragAndDropController dragAndDropController;

    public InventoryPanel inventoryPanel;

}


