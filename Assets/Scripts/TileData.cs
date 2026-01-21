using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenuAttribute(menuName ="Data/Tile Data")]
public class TileData : ScriptableObject
{
    public List<TileBase> tiles;

    public bool plowable;


}
