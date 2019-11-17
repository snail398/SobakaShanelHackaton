using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeroDeath : MonoBehaviour
{
    private Animator _anim;
    private bool _isDead;

    public bool IsDead => _isDead;

    private void Awake()
    {
        _anim = GetComponentInParent<Animator>();
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
        _anim.SetTrigger("Death");
        GameResultHandler.Instance.WinGame();
          //  SceneManager.LoadScene("SampleScene");//Change name of scene???
    }
    public void Lose()
    {
        //play anim
        GameResultHandler.Instance.LoseGame();
    }
}