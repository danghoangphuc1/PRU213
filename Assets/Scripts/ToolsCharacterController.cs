using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{

    Player player;
    Rigidbody2D rgbd2d;

    ToolbarController toolbarController;

    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;

    Vector3Int selectedTilePosion;
    bool selectable;
    private void Awake()
    {
        player = GetComponent<Player>();
        rgbd2d = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolbarController>();
    }

    private void Update()
    {
        ////
        //if (markerManager != null)
        //{
        //    selectedTile();
        //    CanSelectCheck();
        //    Marker();
        //}
        ////
        ///

        selectedTile();
        CanSelectCheck();
        Marker();

        if (Input.GetMouseButtonDown(0))
        {
            //chat
            if (IsPointerOverUI())
                return;
            bool toolUsed = UseToolWorld();
            if (toolUsed)  // ← CHỈ block khi tool THÀNH CÔNG
            {
                return;
            }
            //if (UseToolWorld() == true)
            //{
            //    return;
            //}
            UseToolGrid();
        }
    }

    private void selectedTile()
    {
        selectedTilePosion = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 chacracterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(chacracterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);

    }

    private void Marker()
    {
        //if (markerManager == null || tileMapReadController == null)
        //    return;   // không làm gì nếu đã bị xóa / chưa gán

        //Vector3Int gridPosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        //markerManager.markedCellPosition = gridPosition;

        markerManager.markedCellPosition = selectedTilePosion;


    }

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + player.lastMotionVector * offsetDistance;

        Item item = toolbarController.GetItem;
        if (item == null)
        {
            MessageUI.Instance?.Show("Bạn chưa cầm dụng cụ nào!");   //chat
            return false;
        }
        if (item.onAction == null) 
        {
            //chat
            if (MessageUI.Instance != null)
            {
                if (item.isTool)
                    MessageUI.Instance.Show("Dụng cụ này chưa có hành động!");
                else
                    MessageUI.Instance.Show("Vật phẩm này không phải là dụng cụ.");
            }
            //->
            return false;  
        }

        // CHECK TOOL ĐÚNG TRƯỚC KHI GATHER (quan trọng!)
        ResourceNodeType? requiredNodeType = GetRequiredNodeTypeForItem(item.name);
        if (requiredNodeType.HasValue)
        {
            // Thay dòng check cũ bằng:
            if (!HasCorrectResourceNodeNearby(position))
            {
                MessageUI.Instance?.Show($"**{item.name}** không dùng được ở đây!");
                return false;
            }
        } //chat


        bool complete = item.onAction.OnApply(position);

        return complete;
    }

    // Check vùng quanh có đúng loại ResourceNode không
    // Thay logic cũ bằng:
    private bool HasCorrectResourceNodeNearby(Vector2 worldPos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos, 1.2f);
        Item item = toolbarController.GetItem;

        foreach (var col in colliders)
        {
            ResourceNode node = col.GetComponent<ResourceNode>();
            if (node != null)
            {
                // Axe chỉ cho Tree, Pickaxe chỉ cho Ore
                if ((item.name == "Rìu" && node.nodeType == ResourceNodeType.Tree) ||
                    (item.name == "Cuốc đá" && node.nodeType == ResourceNodeType.Ore))
                {
                    return true;  // Tìm thấy đúng loại node
                }
            }
        }
        return false;
    }


    // Map tên Item → loại ResourceNode nó dùng cho
    private ResourceNodeType? GetRequiredNodeTypeForItem(string itemName)
    {
        return itemName switch
        {
            "Rìu" => ResourceNodeType.Tree,
            "Cuốc đá" => ResourceNodeType.Ore,
            _ => null
        };
    }


    private void UseToolGrid()
    {
        if (selectable == true)
        {
            TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosion);
            TileData tileData = tileMapReadController.GetTileData(tileBase);
            if (tileData != plowableTiles) { return;  }

            if (cropsManager.Check(selectedTilePosion))
            {
                cropsManager.Seed(selectedTilePosion);
            }
            else
            {
                cropsManager.Plow(selectedTilePosion);
            }
        }
    }

    //chat
    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;
        return EventSystem.current.IsPointerOverGameObject(); // mouse (-1)
    }


}
