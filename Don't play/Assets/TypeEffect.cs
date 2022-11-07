using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    Text textRef;
    public string fullText;
    public float typingDelay;
    string currentText;
    string savedText;

    void Start()
    {
        textRef = GetComponent<Text>();
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
