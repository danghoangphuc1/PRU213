using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject pickUpItemPrefab;

    public void SpawnItem(Vector3 poison, Item item, int count)
    {
        GameObject o = Instantiate(pickUpItemPrefab, poison, Quaternion.identity);
        o.GetComponent<PickupItem>().Set(item, count);  
    }

}
