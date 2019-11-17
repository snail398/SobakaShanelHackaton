using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UniRx;

public class VideoScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _player;


    private void Awake()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(_player.length)).Subscribe(_=> MoveToNextScene()).AddTo(this);
    }

    public void MoveToNextScene()
    {
        SceneManager.LoadScene("SampleScene");
    }


}
