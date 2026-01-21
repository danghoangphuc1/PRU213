using UnityEngine;
using TMPro;
using System.Collections;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance;  // Gọi từ mọi nơi: MessageUI.Instance.Show("text")

    [Header("UI")]
    [SerializeField] TMP_Text messageText;
    [SerializeField] float showDuration = 2f;
    [SerializeField] AnimationCurve fadeCurve;  // Optional: fade effect

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            messageText.alpha = 0;  // Ẩn sẵn
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show(string message)
    {
        StopAllCoroutines();  // Dừng message cũ
        StartCoroutine(ShowMessageCoroutine(message));
    }

    IEnumerator ShowMessageCoroutine(string message)
    {
        // Hiện text
        messageText.text = message;
        messageText.alpha = 1;
        messageText.gameObject.SetActive(true);

        // Chờ
        yield return new WaitForSeconds(showDuration);

        // Fade out (optional)
        float fadeTime = 0.5f;
        float elapsed = 0;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            messageText.alpha = Mathf.Lerp(1, 0, elapsed / fadeTime);
            yield return null;
        }

        messageText.gameObject.SetActive(false);
    }
}
