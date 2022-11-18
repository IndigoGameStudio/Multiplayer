using System.Collections;
using UnityEngine;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    TextMeshProUGUI textRef;
    public string fullText;
    public float typingDelay;
    string currentText;
    string savedText;

    void Start()
    {
        textRef = GetComponent<TextMeshProUGUI>();
        savedText = textRef.text;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        while(true)
        {
            for (int i = 0; i <= fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i);
                textRef.text += currentText;
                yield return new WaitForSeconds(typingDelay);
                textRef.text = savedText;
            }
        }
    }
}
