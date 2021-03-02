using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeethovenTasks : MonoBehaviour
{
    public GameBehaviour gameBehaviour;

    //Controle da tarefa
    //O semáforo organiza as tarefas, impedindo-as de ocorrerem todas ao mesmo tempo, permitindo assim que o jogador só possa resolver uma por vez
    public GameObject CanvasTaskBar;
    public GameObject Player;
    private TaskBar myTaskBar;
    public int barProgress = 0;
    public bool startedTask = false;
    public int semaforoControl = 0;

    // Update is called once per frame
    void Update()
    {
        //Beethoven dará apenas 3 tarefas ao longo do jogo
        checkTaskTime(250, 1);
        checkTaskTime(150, 2);
        checkTaskTime(50, 3);
    }

    
    void checkTaskTime(int ini, int semaforo){
        if(gameBehaviour.tempoRestante <= ini && gameBehaviour.tempoRestante >= (ini-1) && semaforoControl != semaforo) semaforoControl = semaforo;    //Indica a tarefa que será feita.
        if(gameBehaviour.tempoRestante <= ini && semaforo == semaforoControl) taskTime();
    }

    void taskTime(){
        //Se o jogador chegar perto de Beethoven, a task aparece
        if(Vector3.Distance(this.transform.position, Player.transform.position) <= 1.5f){
            if(!startedTask){
                startedTask = true;
                setCanvasTaskBar();
            }
        }
        //Se o jogador se afastar de Beethoven, a task some.
        if(Vector3.Distance(this.transform.position, Player.transform.position) >= 1.5f){
            startedTask = false;
        }
    }

    //Seta a Taskbar - zera ela e instancia no objeto myTaskBar para ser usada por essa classe
    //Essa função cria a barra de tarefas dos cachorros através da Unity.
    void setCanvasTaskBar(){
        var newCanvasTaskBar = Instantiate(CanvasTaskBar, new Vector3(0, 0, 0), gameBehaviour.transform.rotation) as GameObject;
        newCanvasTaskBar.name = "CanvasTaskBar";
        newCanvasTaskBar = GameObject.Find("CanvasTaskBar");
        myTaskBar = newCanvasTaskBar.GetComponent<TaskBar>();
        myTaskBar.Dog = GameObject.Find("Beethoven");
        myTaskBar.SetBar(barProgress);  //Inicia a barra com 0 de progresso (ou com o progresso deixado para o jogador pela última vez)
    }

    //Cria o botão da tarefa, atualiza ela conforme é clicada e completa e reseta caso vá até o fim.
    void OnGUI(){
        if(startedTask){
            if(GUI.RepeatButton(new Rect(Screen.width/2 - 75, (Screen.height/2) + 26, 150, 50), "Cuidar de Beethoven!")){
                barProgress++;
                myTaskBar.SetBar(barProgress);
                if(barProgress == myTaskBar.slider.maxValue){
                    gameBehaviour.score += 20;
                    barProgress = 0;
                    startedTask = false;
                    semaforoControl = 0;
                }
            }
        }
    }
}
