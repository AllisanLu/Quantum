using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextScroller : MonoBehaviour
{
    TextMeshProUGUI textmeshpro;
    string fullText;
    public Image space;
    public float waitTime = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        textmeshpro = GetComponent<TextMeshProUGUI>();
        fullText = textmeshpro.text;
        textmeshpro.text = "";
        space.color = Color.clear;

        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        textmeshpro.text = "";

        foreach (char c in fullText.ToCharArray())
        {
            textmeshpro.text += c;
            yield return new WaitForSeconds(waitTime);
        }

        space.color = Color.white;

        yield return null;
    }
}
