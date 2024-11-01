using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
    }
}
