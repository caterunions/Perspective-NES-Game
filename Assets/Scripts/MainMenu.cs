using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// this script will contain multiple methods
// that will be assigned to a button in
// unity's ui system
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int _playScene;
    [SerializeField]
    private GameObject _controlsMenu;
    public void StartGame()
    {
        SceneManager.LoadScene(_playScene, LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowControls()
    {
        _controlsMenu.SetActive(true);
    }
    public void HideControls()
    {
        _controlsMenu.SetActive(false);
    }
}
