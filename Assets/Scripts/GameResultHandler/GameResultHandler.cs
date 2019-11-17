using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class GameResultHandler: MonoBehaviour
{
    private static GameResultHandler _instance;

    public static GameResultHandler Instance => _instance;

    [SerializeField] private Transform _winPanel;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
       // DontDestroyOnLoad(this);
    }

    //boy killed
    public void WinGame()
    {
        _winPanel.gameObject.SetActive(true);
    }

    //pizza delivered
    public void LoseGame()
    {
        SceneManager.LoadScene("MainMenu");//or some scene with info about ur death
    }
}
