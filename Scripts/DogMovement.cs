using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DogMovement : MonoBehaviour
{
        //Destinação
    public Transform path;
    public List<Transform> locations;
    public float timeForNextMovement;
    public float startTimeForNextMovement;

    private AIDestinationSetter Destination;
    private int locationIndex = 0;
    private bool hasReachedDestination = false;

    //Animações
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        Destination = GetComponent<AIDestinationSetter>();
        //Inicia o movimento
        InitializePath();
        MoveToNextLocation();
    }

    void Update()
    {
        //Parado
        if(timeForNextMovement > 0.0f && hasReachedDestination){
            timeForNextMovement -= Time.deltaTime;
            if(Vector3.Distance(transform.position, Destination.target.position) <= 0.001f) animator.SetFloat("Speed", 0);
        }
        else{       //Andando
            timeForNextMovement = startTimeForNextMovement;
            hasReachedDestination = false;
            //Define o próximo destino
            if(Vector3.Distance(transform.position, Destination.target.position) <= 0.2f){
                hasReachedDestination = true;
                animator.SetFloat("Speed", 1);
                MoveToNextLocation();
            }
        }

        //Vira a animação do cachorro da direita pra esquerda e vice versa
        if(transform.position.x - Destination.target.position.x < -0.2f && transform.rotation.y < 0.2f){
            this.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        }
        if(transform.position.x - Destination.target.position.x > 0.2f && transform.rotation.y > 0.2f){
            this.transform.Rotate(0.0f, -180.0f, 0.0f, Space.Self);
        }
    }

    public void SetDogMoveTimer(int time){
        startTimeForNextMovement = time;
    }

    void InitializePath(){
        foreach(Transform child in path){
            locations.Add(child);
        }
    }

    void MoveToNextLocation(){
        if(locations.Count == 0) return;
        Destination.target = locations[locationIndex].transform;
        locationIndex = (locationIndex + 1) % locations.Count;
    }
}
