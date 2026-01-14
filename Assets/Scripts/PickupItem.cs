using UnityEngine;

public class PickupItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f;

    public Item item;
    public int count = 1;


    private void Start()   
    {
        if (GameManager.instance != null && GameManager.instance.player != null)
        {
            player = GameManager.instance.player.transform;
        }
        else
        {
            Debug.LogError("GameManager hoặc player chưa sẵn sàng", this);
        }
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;

    }

    private void Update()
    {
        if (player == null) return;

        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        if (distance < 0.1f)
        {
            if (GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(item, count);

                // thêm dòng này để kho vẽ lại ngay
                if (GameManager.instance.inventoryPanel != null)
                    GameManager.instance.inventoryPanel.Refresh();
            }
            else
            {
                Debug.LogWarning("No inventory container attched to the game manager");
            }

                Destroy(gameObject);
        }
    }
}
