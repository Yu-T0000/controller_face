using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Enemy : MonoBehaviour
{
    private Animator D_anim = null;

    void Start()
    {
        D_anim = GetComponent<Animator>();
        Debug.Log("ダメージ受けた");
        anime();
    }
    void Update(){
        bool state = GameObject.Find("Damagemanager").GetComponent<Damagecheck>().damage;
        if(state == false){
            Debug.Log("消えるよ");
            D_anim.SetBool("is_Damaged", false);
        }
    }
    public void OnAnimationEnd()
    {
        Destroy( gameObject );
    }
    async void anime()
    {
        D_anim.SetBool("is_Damaged", true);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        GetComponent<Rigidbody2D>().WakeUp();
    }
    public void Init(){
        transform.localPosition = new Vector3(0,0,0);
    }
}
