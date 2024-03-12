using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expression : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator face = null;
    private float Feeling; //ご機嫌ゲージ 0~100
    private float time = 0; 
    public string Shake = "";



    
    void Start()
    {
        face = GetComponent<Animator>();
        Feeling = GameObject.Find("Emotion").GetComponent<EmotionCheck>().meter;
        string state = GameObject.Find("Emotion").GetComponent<EmotionCheck>().feeling;
    }

    // Update is called once per frame
    void Update()
    {
        Shake = GameObject.Find("Emotion").GetComponent<EmotionCheck>().Shake;

        Feeling = GameObject.Find("Emotion").GetComponent<EmotionCheck>().meter;
        string state = GameObject.Find("Emotion").GetComponent<EmotionCheck>().feeling;
        if(Shake == "Shake"){
            face.SetBool("Stop", true);
        }
        else{
            face.SetBool("Stop", false);
        }
        if(state == "Neutral"){
            face.SetBool("Angry", false);
            face.SetBool("Smile", false);
            if(time == 240){
                face.SetBool("Brink", true);
                face.SetTrigger("change");
            }
            else if(time >= 370){
                face.SetBool("Brink", false);
                face.ResetTrigger("change");
                time = 0;
            }
            time += 1;
            
            
        }
        else if(state == "Bad"){
            face.SetBool("Brink", false);
            face.SetBool("Angry", true);
             face.SetTrigger("change");
             time = 0;
             
        }
        else if(state == "Happy"){
            face.SetBool("Brink", false);
            face.SetBool("Smile", true);
            face.SetTrigger("change");
            time = 0;
        }
}
}