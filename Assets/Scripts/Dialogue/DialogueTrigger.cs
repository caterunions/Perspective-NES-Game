using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private TextAsset _dialogue;
    [SerializeField]
    private bool _dialogueOnStart;
    [SerializeField]
    private DialogueManager _dialogueManager;

    private bool _dialogueIsPlaying = false;
    private void Update ()
    {
        if (_dialogueOnStart && !_dialogueIsPlaying)
        {
            _dialogueManager.StartDialogue(_dialogue);
            _dialogueIsPlaying = true;
        }
    }
}
