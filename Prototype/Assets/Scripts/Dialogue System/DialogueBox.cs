using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private TextAsset script;

    [SerializeField]
    private AudioClip voiceClip;

    [SerializeField]
    private AudioSource source;

    private Queue<string> scriptLines;
    private string[] scriptTemp;

    private int dialogueCounter = 0;

    [SerializeField]
    private Dialogue[] dialogues;

    //Maybe have an array of texture2d here for the background

    private void Awake()
    {
        TouchEvents.OnSingleTouch += PlayNextDialogue;
    }
    void Start()
    {
        scriptLines = new Queue<string>();
        scriptTemp = new string[10];
        ReadInScript();
        source.clip = dialogues[0].voiceClip;
        PlayDialogue();
    }


    private void ReadInScript()
    {
        scriptTemp = script.text.Split('\n');
        //Debug.LogWarning(scriptLines.Count);
        for (int i = 0; i < scriptTemp.Length; i++)
        {
            scriptLines.Enqueue(scriptTemp[i]);
            //dialogues[i].dialogueText = scriptTemp[i];
        } 
    }

    private void PlayDialogue()
    {
        source.Play();
        //while (scriptLines.Count > 0)
        //{
        //    //Display script lines and bring up the sentence one char at a time
        //}
        dialogueText.text = dialogues[0].dialogueText;
    }

    private void PlayNextDialogue(Touch t)
    {
        dialogueCounter++;
        source.clip = dialogues[dialogueCounter].voiceClip;
        source.Play();
        dialogueText.text = dialogues[dialogueCounter].dialogueText;

    }

    IEnumerator AnimateDialogue()
    {
        yield return 0.1f;
    }
}
