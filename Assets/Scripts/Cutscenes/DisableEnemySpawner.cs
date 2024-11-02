using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemySpawner;

    // Start is called before the first frame update
    void Awake()
    {
        //_enemySpawner.SetActive(false);
        _enemySpawner.GetComponent<EnemySpawner>().enabled = false;
    }
}
