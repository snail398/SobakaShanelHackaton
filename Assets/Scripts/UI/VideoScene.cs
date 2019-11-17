using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoScene : MonoBehaviour
{
    public void MoveToNextScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
