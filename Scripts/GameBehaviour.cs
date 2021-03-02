using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{

    //Cria caixa de texto de Beethoven
    private string beethovenBoxText;
    private int beethovenBoxX = 400;
    private int beethovenBoxY = 30;

    //Cria caixa de texto de Laika
    private string laikaBoxText;
    private int laikaBoxX = 400;
    private int laikaBoxY = 30;

    //Cria caixa de texto de Galdalf
    private string gandalfBoxText;
    private int gandalfBoxX = 400;
    private int gandalfBoxY = 30;

    //Variáveis para controle de score e fim do jogo
    public int score = 0;
    public float tempoRestante = 300f;
    public bool endGameScreen = false;

    //Cada um dos cachorros funciona independente dos outros.
    public BeethovenTasks beethovenTasks;
    public LaikaTasks laikaTasks;
    public GandalfTasks gandalfTasks;

    public DogMovement BeethovenMove;
    public DogMovement LaikaMove;
    public DogMovement GandalfMove;

    void Start(){
        BeethovenMove.startTimeForNextMovement = 40f;
        LaikaMove.startTimeForNextMovement = 15f;
        GandalfMove.startTimeForNextMovement = 23f;
    }

    // Update is called once per frame
    void Update()
    {
        //Atualiza textos nas caixas de texto
        //Usa os semáforos para descobrir se alguma tarefa de cada uma das threads está pendente ou não.
        if(beethovenTasks.semaforoControl > 0) trocarBeethovenBoxText("Beethoven precisa de seus curativos trocados!", 325, 30);
        else trocarBeethovenBoxText("Beethoven está bem!", 200, 30);

        if(laikaTasks.semaforoControl > 0) trocarLaikaBoxText("Laika cavou um buraco!", 250, 30);
        else trocarLaikaBoxText("Laika está comportada... por hora", 275, 30);

        if(gandalfTasks.semaforoControl > 0) trocarGandalfBoxText("Gandalf sujou o chão!", 200, 30);
        else trocarGandalfBoxText("Gandalf está limpo! Incrível!", 250, 30);

        //Tempo de jogo!
        if(tempoRestante > 0){
            tempoRestante -= Time.deltaTime;
        }
        else{
            Time.timeScale = 0;
            endGameScreen = true;
            trocarBeethovenBoxText("O dia acabou!", 120, 30);
            Time.timeScale = 0f;
        }
    }

    //Atualiza o texto e as dimensões da caixa de Beethoven
    void trocarBeethovenBoxText(string newLabel, int newBoxX, int newBoxY){
        beethovenBoxText = newLabel;
        beethovenBoxX = newBoxX;
        beethovenBoxY = newBoxY;
    }

    //Atualiza o texto e as dimensões da caixa de Laika
    void trocarLaikaBoxText(string newLabel, int newBoxX, int newBoxY){
        laikaBoxText = newLabel;
        laikaBoxX = newBoxX;
        laikaBoxY = newBoxY;
    }

    //Atualiza o texto e as dimensões da caixa de Gandalf
    void trocarGandalfBoxText(string newLabel, int newBoxX, int newBoxY){
        gandalfBoxText = newLabel;
        gandalfBoxX = newBoxX;
        gandalfBoxY = newBoxY;
    }


    //Gera a IU (Interface de Usuário) do jogo
    void OnGUI(){
        GUI.Box(new Rect (20, 20, 150, 25), "Pontuação: " + score);
        GUI.Box(new Rect(20, Screen.height - 50, beethovenBoxX, beethovenBoxY), beethovenBoxText);
        GUI.Box(new Rect(20, Screen.height - 81, laikaBoxX, laikaBoxY), laikaBoxText);
        GUI.Box(new Rect(20, Screen.height - 112, gandalfBoxX, gandalfBoxY), gandalfBoxText);

        if(endGameScreen){
            if(GUI.Button(new Rect(Screen.width/2 - 75, (Screen.height/2) + 26, 150, 50), "Sair do Jogo")){
                Application.Quit();
            }
        }
    }
}
