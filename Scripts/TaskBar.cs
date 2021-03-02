using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    public GameObject Dog;
    public GameObject Player;
    public Slider slider;

    void Start(){
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Dog.transform.position, Player.transform.position) >= 1.5f){
            Destroy(this.gameObject);
        }
    }

    public void SetMaxProgress(int barMax){
        slider.maxValue = barMax;
        slider.value = 0;
    }

    public void SetBar(int barProgress){
        slider.value = barProgress;
    }
}
