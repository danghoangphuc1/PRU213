using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] Transform headSlot;      // gán HeadSlot
    [SerializeField] GameObject nonLaPrefab;  // gán prefab nón lá

    GameObject currentHat;

    public void EquipNonLa()
    {
        if (currentHat != null) Destroy(currentHat);

        currentHat = Instantiate(nonLaPrefab, headSlot);
        currentHat.transform.localPosition = Vector3.zero;   // đúng vị trí HeadSlot
        currentHat.transform.localRotation = Quaternion.identity;
    }

    public void UnequipHat()
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
            currentHat = null;
        }
    }
}
