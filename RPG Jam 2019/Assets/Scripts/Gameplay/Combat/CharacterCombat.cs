using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    Animator animator;
    ParticleSystem ps;

    public string characName;//TODO: set the name automatically

    public bool placeholderAttacker; //to determine if we want to attack with this character or not
    public CharacterCombat enemy;

    ActInfo currentAct;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        //be sure that animator does not plays the same animation twice or more
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            animator.SetBool("isAttacking", false);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("damaged"))
            animator.SetBool("isDamaged", false);
    }

    public void act(ActInfo actInfos) {
        currentAct = actInfos;
        switch (currentAct.actType) {
            case (ActType.Attack):
                attack();
                break;
            case (ActType.Skill):
                useSkill();
                break;
            default:
                Debug.Log("WARNING: Act Type " + actInfos.actType + "has no animation ");
                break;
        }
    }

    public void attack() {
        animator.SetBool("isAttacking", true);
    }

    public void useSkill() {
        damageEnemy();
        //no animation yet
        Debug.Log("no animation for skill");//TODO
    }

    public void damageEnemy() {
        //TODO: voir comment faire pour le heal
        foreach(DamagesInfo dmgInfo in currentAct.damagesInfo) {
            foreach(Character target in dmgInfo.targets) {
                CombatManager.i.getCharacterCombatFromName(target.name).getDamaged(0);
            }
        }
    }

    public void damageAnimationFinished() {
        CombatManager.i.damageAnimationFinished(characName);
    }

    //we need to notify the attacker when we have taken the damage
    public void getDamaged(int receivedDamage) {
        animator.SetBool("isDamaged", true);
        ps.Play();
    }
}
