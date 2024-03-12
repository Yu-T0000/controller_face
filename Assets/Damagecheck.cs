using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uOSC;

public class Damagecheck : MonoBehaviour
{
    public Enemy enemyPrefab;

    public bool damage = false;

    void Awake()
    {
    }

    public void OnDataReceived(Message message)
    {
        if (message.address == "/damage")
        {
            OnMessage_A();
        }
        else if (message.address == "/damage2")
        {
            OnMessage_B();
        }
        else if (message.address == "/pause")
        {
             damage = false;
        }
    }


    private void OnMessage_A()
    {
        
            Debug.Log("received");
            var enemy = Instantiate( enemyPrefab );
            enemy.Init();
            damage = true;
        
    }
    private void OnMessage_B()
    {
        
            Debug.Log("received2");
            damage = false;
            return;
        
    }

}
