using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this script handle:
// 1. displaying each page that will
// explain how to play the game.
// 2. methods for buttons to
// display each page one
// at a time.
public class ControlsMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _titleText;
    [SerializeField]
    private TextMeshProUGUI _bodyText;
    [SerializeField]
    private Animator _gameplayImg;

    private int _index;
    [SerializeField]
    private int _maxindex;

    private void Start()
    {
        _titleText.text = string.Empty;
        _bodyText.text = string.Empty;
    }

    private void Update()
    {
        GetSwitch();
        if(_index > _maxindex)
        {
            _index = 0;
        }
        else if (_index < 0)
        {
            _index = _maxindex;
        }
    }

    private void GetSwitch()
    {
        switch (_index)
        {
            case 0:
                _titleText.text = "Movement";
                _bodyText.text = "Use the WASD keys to move.";
                // _gameplayImg.Play(INSERT ANIM HERE);
                break;
            case 1:
                _titleText.text = "Aiming";
                _bodyText.text = "Use your mouse to aim your spells.";
                // _gameplayImg.Play(INSERT ANIM HERE);
                break;
            case 2:
                _titleText.text = "Spells";
                _bodyText.text = "Left click to use your regular spell. Right click or press SPACE to use your special spell.";
                // _gameplayImg.Play(INSERT ANIM HERE);
                break;
            case 3:
                _titleText.text = "Switching spells";
                _bodyText.text = "Scroll the mouse to switch your spells.";
                // _gameplayImg.Play(INSERT ANIM HERE);
                break;
            case 4:
                _titleText.text = "Mana & Health";
                _bodyText.text = "Keep an eye on your Mana and Health bars. When you cast a special spell, it will consume mana.";
                break;
        }
    }
    // Buttons
    public void BackPage()
    {
        _index--;
    }

    public void NextPage()
    {
        _index++;
    }
}
