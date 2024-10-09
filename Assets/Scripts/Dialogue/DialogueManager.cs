using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject _dialoguePanel;
    [SerializeField]
    private TextMeshProUGUI _dialogueText;
    [SerializeField]
    private GameObject _continueIcon; // displays once line is completed

    private TextAsset _dialogue;
    private string[] _allLines; // array of each line in the text file
    private int _lineLength; // length of current line

    private int _currentLine; // line in text file
    private int _currentChar; // current character in the line
    private bool _dialogueIsPlaying;
    [SerializeField] // remove once _lineLimit is figured out
    private int _lineLimit; // max num of characters before going to next line

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There's more than one Dialogue Manager in the scene!");
        }
        instance = this;
    }

    private static DialogueManager GetInstance()
    {
        
        return instance;
    }


    private void Start()
    {
        _dialoguePanel.SetActive(false);
        _continueIcon.SetActive(false);
        _dialogueIsPlaying = false;
    }

    private void Update()
    {
        if (!_dialogueIsPlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset DialogueText)
    {
        Debug.Log("EnterDialogueMode");
        // * remember to add this later:
        // pass a bool to check if auto continue dialogue is enabled or not
        // otherwise it is not auto
        _dialogue = new TextAsset(DialogueText.text);
        _dialogueIsPlaying = true;
        _dialoguePanel.SetActive(true);

        _dialogueText.text = _dialogue.ToString();
    }

    private void ContinueDialogue()
    {
        

    }

    private void ExitDialogueMode()
    {
        _dialogueIsPlaying = false;
        _dialoguePanel?.SetActive(false);
        _dialogueText.text = "";
    }
}
