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

    private void Awake()
    {
         _dialogueManager = GetComponent<DialogueManager>();
    }

    // Start is called before the first frame update
    void Start()
    { 
       

        if (_dialogueOnStart)
        {
            _dialogueManager.EnterDialogueMode(_dialogue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
