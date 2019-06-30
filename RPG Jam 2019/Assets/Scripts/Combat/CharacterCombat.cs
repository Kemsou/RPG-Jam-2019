using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    Animator animator;
    ParticleSystem ps;

    public bool placeholderAttacker; //to determine if we want to attack with this character or not
    public CharacterCombat enemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A) && placeholderAttacker) {
            attack();
        }

        //be sure that animator does not plays the same animation twice or more
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            animator.SetBool("isAttacking", false);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("damaged"))
            animator.SetBool("isDamaged", false);
    }

    public void attack() {
        animator.SetBool("isAttacking", true);
    }

    public void damageEnemy() {
        enemy.getDamaged(0);
    }

    public void getDamaged(int receivedDamage) {
        animator.SetBool("isDamaged", true);
        ps.Play();
    }
}
