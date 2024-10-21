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
                _titleText.text = "PAGE 1";
                _bodyText.text = "1";
                // _gameplayImg.Play(INSERT ANIM HERE);
                break;
            case 1:
                _titleText.text = "PAGE 2";
                _bodyText.text = "2";
                // _gameplayImg.Play(INSERT ANIM HERE);
                break;
            case 2:
                _titleText.text = "PAGE 3";
                _bodyText.text = "3";
                // _gameplayImg.Play(INSERT ANIM HERE);
                break;
            case 3:
                _titleText.text = "PAGE 4";
                _bodyText.text = "4";
                // _gameplayImg.Play(INSERT ANIM HERE);
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
