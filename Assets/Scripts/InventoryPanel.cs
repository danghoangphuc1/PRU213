using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.Onclick(inventory.slots[id]);
        Show();
    }


}
