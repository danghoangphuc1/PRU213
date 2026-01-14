using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool opened;
    public override void Interact(Character character)
    {
        opened = !opened;                   
        closedChest.SetActive(!opened);     
        openedChest.SetActive(opened);
    }


}
