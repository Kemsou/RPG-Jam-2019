//Original Scripts by IIColour (IIColour_Spectrum)

using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 speed;
    private float position;
    private Rigidbody2D rb2D;

    private Animator animator;


    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("VerticalAxis", vertical);
        animator.SetFloat("HorizontalAxis", horizontal);

        //Debug.Log(GameSaves.Instance.gamesStates.entitys[0].name);
        var move = new Vector3(horizontal, vertical, 0);
        move = move.normalized * speed * Time.deltaTime;
        rb2D.MovePosition(transform.position + move);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

    }
}