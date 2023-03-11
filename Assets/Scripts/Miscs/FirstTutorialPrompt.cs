// The codes in this .cs file is written by me
// The comments are written by me and showing my understanding of the codes in this .cs file

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FirstTutorialPrompt : MonoBehaviour
{
    [SerializeField] Text prompt; // to reference the Tutorial Prompt Texts

    void Start()
    {
        StartCoroutine(firstTutorialScreen());

        IEnumerator firstTutorialScreen()
        {
            prompt.gameObject.SetActive(true);

            yield return new WaitForSeconds(3.75f);

            prompt.gameObject.SetActive(false);
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            prompt.gameObject.SetActive(false);
        }
    }
}
