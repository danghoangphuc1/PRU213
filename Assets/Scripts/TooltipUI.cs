using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    [SerializeField] RectTransform panel;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descText;
    [SerializeField] Vector2 offset = new Vector2(20, -20);

    void Awake()
    {
        Instance = this;

        panel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (panel.gameObject.activeSelf)
            panel.position = (Vector2)Input.mousePosition + offset;
    }

    public void Show(Item item)
    {
        if (item == null) return;
        nameText.text = item.DisplayNameVN;
        descText.text = item.DescriptionVN;
        panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        panel.gameObject.SetActive(false);
    }
}
