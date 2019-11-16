using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeroDeath : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
            Die();
    }
    public void Die()
        {
            //play dying anim;
            //Destroy(this.gameObject);
            SceneManager.LoadScene("SampleScene");//Change name of scene???
        }
}