using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dialog : MonoBehaviour {

    public Text dialogText;
    public GameObject continueButton;
    public Animator textFadeAnimation;

    [Tooltip("Max 50 characters")]
    public string[] sentences;

    [Tooltip("Float in seconds, lower = faster")]
    public float typingSpeed;

    private int currentIndex;

    private void Start()
    {
        dialogText.text = "";
        StartCoroutine(TypeEffect());
    }

    private void Update()
    {
        if (dialogText.text == sentences[currentIndex])
            continueButton.SetActive(true);
    }

    IEnumerator TypeEffect()
    {
        textFadeAnimation.SetTrigger("Fade_In");

        foreach (char letter in sentences[currentIndex].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        textFadeAnimation.SetTrigger("TextDone");
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (currentIndex < sentences.Length - 1)
        {
            currentIndex++;
            dialogText.text = "";
            StartCoroutine(TypeEffect());
        }
        else
        {
            dialogText.text = "";
            continueButton.SetActive(false);
        }
    }
}