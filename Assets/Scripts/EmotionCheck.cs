using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OscCore;


public class EmotionCheck : MonoBehaviour
{
    public string feeling = "Neutral";
    public float meter = 50;
    private Vector3 Acceleration;
    private Vector3 preAcceleration;
    float DotProduct;
    public string Shake = "";
    float time;

    OscClient client;


    float level;
    BatteryStatus status;

    public void Awake()
    {
        string serverip = PlayerPrefs.GetString("IP");
        client = new OscClient(serverip, 9000);
    }

    // Update is called once per frame
    void Update()
    {
        var gauge = meter/100;
        status = SystemInfo.batteryStatus;
        bool state = GameObject.Find("Damagemanager").GetComponent<Damagecheck>().damage;

        preAcceleration = Acceleration;
        Acceleration = Input.acceleration;
        DotProduct = Vector3.Dot(Acceleration, preAcceleration);
        if(state){
            Shake = "Shake";
        }

        if (DotProduct < 0 && time == 0){
            Shake = "Shake";
        }
        else if(Shake == "Shake"){
            time += Time.deltaTime;
            meter -= Time.deltaTime * 10;
            if(time > 2){
            Shake = "";
            time=0;}
            if(meter<= 0)meter = 0;
        }
        else{
            Shake = "";
        }

        if (status == BatteryStatus.Charging || status == BatteryStatus.Full){
            if(meter < 100){
            meter += 0.12f;
            }
        }

        GetComponent<Renderer>().material.SetFloat("_Ratio", gauge);
        if(meter <= 35){
            feeling = "Bad";
            client.Send("/feeling", "Bad");
        }
        else if(meter >= 65){
            feeling = "Happy";
            client.Send("/feeling", "Good");
        }
        else{
            feeling = "Neutral";
            client.Send("/feeling", "Neutral");
        }
        
    }
    void shake(){
        Shake = "Shake";
            for(int i = 0; i < 120; i++){
                meter -= i/100;
                if(meter<= 0)meter = 0;
            }
        Shake = "";
    }
}
