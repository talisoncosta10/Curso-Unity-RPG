using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            NextSentence();
        }
    }

    public void Speech(string[] txt, string actorName)
    {
        dialogueObj.SetActive(true);
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    } 

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void NextSentence()
    {
        if(speechText.text == sentences[index].ToString())
        {
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine (TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
            }
        }
    }
}
