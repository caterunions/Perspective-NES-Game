using UnityEngine;
using UnityEngine.Playables;

// set timeline speed to 0 if dialogue is playing
public class TimelineDialogue : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _pDirector;
    [SerializeField]
    private DialogueManager _dialogueManager;
    private float _currentSpeed;

    private void Start()
    {
        _pDirector = gameObject.GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        CheckSpeed();
        _pDirector.playableGraph.GetRootPlayable(0).SetSpeed(_currentSpeed); 
    }

    private void CheckSpeed()
    {
        if (_dialogueManager._dialogueIsPlaying)
        {
            _currentSpeed = 0;
        }
        else
        {
            _currentSpeed = 1;
        }
    }
}
