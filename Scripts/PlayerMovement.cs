using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 4.5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement; //Vector 2 pega um vetor 2D

    // Update is called once per frame
    void Update()
    {
        //Input (Teclado) - Pega os comandos
        movement.x = Input.GetAxisRaw("Horizontal"); //Pega teclas A, D, Direita e Esquerda
        movement.y = Input.GetAxisRaw("Vertical"); //Pega teclas W, S, Cima e Baixo

        //Funciona com a Unity para fazer controlar as animações de movimento.
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //Determina movimento do personagem e velocidade do movimento.
        rb.MovePosition(rb.position + movement * playerMoveSpeed * Time.fixedDeltaTime);
    }
}
