using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GandalfTasks : MonoBehaviour
{
    public GameBehaviour gameBehaviour;

    //Controle da tarefa
    //O semáforo organiza as tarefas, impedindo-as de ocorrerem todas ao mesmo tempo, permitindo assim que o jogador só possa resolver uma por vez
    public GameObject CanvasTaskBar;
    public GameObject Player;
    public GameObject Caca;
    private TaskBar myTaskBar;
    public int barProgress = 0;
    public bool startedTask = false;
    public int semaforoControl = 0;

    // Update is called once per frame
    void Update()
    {
        //Gandalf dará 9 tarefas ao longo do jogo.
        checkTaskTime(280, 1);
        checkTaskTime(260, 2);
        checkTaskTime(210, 3);
        checkTaskTime(180, 4);
        checkTaskTime(130, 5);
        checkTaskTime(110, 6);
        checkTaskTime(90, 7);
        checkTaskTime(60, 8);
        checkTaskTime(15, 9);
    }

    
    void checkTaskTime(int ini, int semaforo){
        if(gameBehaviour.tempoRestante <= ini && gameBehaviour.tempoRestante >= (ini-1) && semaforoControl != semaforo) {
            semaforoControl = semaforo;    //Indica a tarefa que será feita.
            var newCaca = Instantiate(Caca, this.transform.position, gameBehaviour.transform.rotation) as GameObject;
            newCaca.name = ("Caca" + semaforoControl);
            newCaca = GameObject.Find("Caca" + semaforoControl);
        }
        if(gameBehaviour.tempoRestante <= ini && semaforo == semaforoControl) taskTime();
    }

    void taskTime(){
        //Se o jogador chegar perto, a task aparece
        if(Vector3.Distance(GameObject.Find("Caca" + semaforoControl).transform.position, Player.transform.position) <= 0.7f){
            if(!startedTask){
                startedTask = true;
                setCanvasTaskBar();
            }
        }
        //Se o jogador se afastar, a task some.
        if(Vector3.Distance(GameObject.Find("Caca" + semaforoControl).transform.position, Player.transform.position) >= 1.5f){
            startedTask = false;
        }
    }

    //Seta a Taskbar - zera ela e instancia no objeto myTaskBar para ser usada por essa classe
    void setCanvasTaskBar(){
        var newCanvasTaskBar = Instantiate(CanvasTaskBar, new Vector3(0, 0, 0), gameBehaviour.transform.rotation) as GameObject;
        newCanvasTaskBar.name = "CanvasTaskBar";
        newCanvasTaskBar = GameObject.Find("CanvasTaskBar");
        myTaskBar = newCanvasTaskBar.GetComponent<TaskBar>();
        myTaskBar.Dog = GameObject.Find("Caca" + semaforoControl);
        myTaskBar.SetBar(barProgress);
    }

    //Cria o botão da tarefa, atualiza ela conforme é clicada e completa e reseta caso vá até o fim.
    void OnGUI(){
        if(startedTask){
            if(GUI.Button(new Rect(Screen.width/2 - 75, (Screen.height/2) + 26, 150, 50), "Limpar!")){
                barProgress += 50;
                myTaskBar.SetBar(barProgress);
                if(barProgress == myTaskBar.slider.maxValue){
                    gameBehaviour.score += 8;
                    barProgress = 0;
                    startedTask = false;
                    Destroy(GameObject.Find("Caca" + semaforoControl));
                    Destroy(GameObject.Find("CanvasTaskBar"));
                    semaforoControl = 0;
                }
            }
        }
    }
}
