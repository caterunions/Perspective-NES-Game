using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this script will be in charge of:
// 1. splitting the .txt file into a string array
// given by a dialogue trigger.
// 2. type out each character in the current line
// 3. player input for continuing or exiting dialogue
public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField]
    private GameObject _dialoguePanel;
    [SerializeField]
    private TextMeshProUGUI _displayText;
    [SerializeField]
    private GameObject _continueIcon; // displays once line is completed
    [SerializeField]
    private Animator _animator;

    private TextAsset _currentDialogue;
    private Coroutine _displayLineCo;
    private string[] _allLines;
    private int _currentLine; // line in text file
    private bool _canContinueDia = false;
    public bool _dialogueIsPlaying = false;
   
    [Header("Typing Speed")]
    [SerializeField]
    private float _typingSpeed;
    private float _currentTypingSpeed;

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There's more than one Dialogue Manager in the scene!");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        _currentTypingSpeed = _typingSpeed;
        _dialogueIsPlaying = false;
        _dialoguePanel.SetActive(false);
        _continueIcon.SetActive(false);
        _displayText.text = string.Empty;
    }

    private void Update()
    {
        if (!_dialogueIsPlaying)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckDialogueState();
        }
    }

    private void CheckDialogueState()
    {
        if (_dialogueIsPlaying)
        {
            if (_canContinueDia)
            {
                _currentTypingSpeed = _typingSpeed;
                ContinueDialogue();
            }
            else
            {
                // skip dialogue
                _currentTypingSpeed = 0;
            }
        }
    }

    public void StartDialogue(TextAsset _textDoc)
    {
        _animator.Play("DP-EnterDialogue");
        _currentLine = 0;
        _currentTypingSpeed = _typingSpeed;
        _currentDialogue = new TextAsset(_textDoc.text);
        // get dialogue lines and put it into an array
        _allLines = _currentDialogue.text.Split(">> ");
        _dialogueIsPlaying = true;
        _dialoguePanel.SetActive(true);
        GetDialogueIndex();
        ContinueDialogue();
    }

    private void GetDialogueIndex()
    {
        for (int i = 0; i < _allLines.Length; i++)
        {
            i = _currentLine;
            break;
        }
    }

    private IEnumerator ExitDialogue()
    {
        _animator.Play("DP-ExitDialogue");
        yield return new WaitForSeconds(0.2f);
        _dialogueIsPlaying = false;
        _dialoguePanel.SetActive(false);
        _displayText.text = "";
    }

    private void ContinueDialogue()
    {
        if(_allLines[_currentLine] != null)
        {
            if (_currentLine < _allLines.Length - 1)
            {
                if(_displayLineCo != null)
                {
                    StopCoroutine(_displayLineCo);
                }
                _currentLine++;
                _displayLineCo = StartCoroutine(DisplayLine());
            }
            else
            {
                StartCoroutine(ExitDialogue());
            }
        } 
    }

    private IEnumerator DisplayLine()
    {
        _displayText.text = ""; // empty text
        _continueIcon.SetActive(false); // hide item
        _canContinueDia = false;
        // type each letter one at a time
        foreach (char letter in _allLines[_currentLine].ToCharArray())
        {
            _displayText.text += letter;
            yield return new WaitForSeconds(_currentTypingSpeed);
        }
        _continueIcon.SetActive(true);
        // dialogue can continue
        _canContinueDia = true;
    }
}
