using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// this script will contain multiple methods
// that will be assigned to a button in
// unity's ui system
public class MainMenu : MonoBehaviour
{
    //[SerializeField]
    //private int _playScene;
    [SerializeField]
    private GameObject _titleScreen;
    [SerializeField]
    private GameObject _controlsMenu;

    private void Start()
    {
        _controlsMenu.SetActive(false);
    }
    public void StartGame()
    {
        Debug.Log("frog");
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowControls()
    {
        _controlsMenu.SetActive(true);
        _titleScreen.SetActive(false);
    }
    public void HideControls()
    {
        _controlsMenu.SetActive(false);
        _titleScreen.SetActive(true);
    }
}
