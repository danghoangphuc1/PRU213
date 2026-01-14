using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            panel.SetActive(!panel.activeInHierarchy);
        }
    }

}
