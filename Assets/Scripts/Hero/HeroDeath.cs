using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeroDeath : MonoBehaviour
{
    private bool _isDead;

    public bool IsDead => _isDead;

    private void Awake()
    {
        _isDead = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
            Die();
        else if (collision.gameObject.layer == 11)
            Lose();
    }
    public void Die()
        {
        //play dying anim;
        //Destroy(this.gameObject);
        _isDead = true;
            SceneManager.LoadScene("SampleScene");//Change name of scene???
        }
    public void Lose()
    {
        //play anim
        SceneManager.LoadScene("MainMenu");//or some scene with info about ur death
    }
}