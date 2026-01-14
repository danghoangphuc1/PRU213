using UnityEngine;

public class TalkInteract : Interactable
{
    [SerializeField] DialogueContainer dialogue;

    public override void Interact(Character character)
    {
        if (GameManager.instance.dialogueSystem.IsBusy)
        {
            return;
        }
        GameManager.instance.dialogueSystem.Initialize(dialogue);
    }
}
