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


    void Start()
    {

    }

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Debug.Log(GameSaves.Instance.gamesStates.entitys[0].name);
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        move = move.normalized * speed * Time.deltaTime;
        rb2D.MovePosition(transform.position + move);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

    }
}