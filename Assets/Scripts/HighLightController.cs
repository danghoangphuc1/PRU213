using UnityEditor;
using UnityEngine;

public class HighLihgtController : MonoBehaviour
{
    [SerializeField] GameObject highlighter;

    GameObject currentTarget;

    public void Highlight(GameObject target)
    {
        currentTarget = target;
        Transform point = target.transform.Find("HighlightPoint");
        Vector3 pos = point != null ? point.position : target.transform.position;
        Highlight(pos);
    }


    public void Highlight(Vector3 position)
    {
        highlighter.SetActive(true);
        highlighter.transform.position = position;

    }

    public void Hide()
    {
        currentTarget = null;
        highlighter.SetActive(false);
    }

}
