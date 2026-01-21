using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;

    public ToolAction onAction;


    public bool isTool; //chat

    //chat
    [SerializeField] string displayNameVN;
    [SerializeField][TextArea] string descriptionVN;

    public string DisplayNameVN => string.IsNullOrEmpty(displayNameVN) ? name : displayNameVN;
    public string DescriptionVN => descriptionVN;
    //->



}
