using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    // Queue so stuff can get added in order
    private Queue<string> sentences;
    
    public Text dialogText;

    [Tooltip("Float in seconds per character, lower = faster")]
    public float typingSpeed;

    public Animator animator;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(DialogObject dialogObject)
    {
        animator.SetBool("isOpen", true);
        sentences.Clear();

        //Add all sentences from the dialog object to the queue
        foreach (string sentence in dialogObject.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //No more sentencse in the queue remaining
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string currentSentence = sentences.Dequeue();
        //Stop if user skips sentences too fast
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }
    
    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialog()
    {
        animator.SetBool("isOpen", false);
    }
}