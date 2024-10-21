using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform _cursor;
    public Transform Cursor => _cursor;

    private void Awake()
    {
        Instance = this;
    }
}
