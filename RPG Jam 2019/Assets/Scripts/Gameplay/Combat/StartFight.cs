using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFight : MonoBehaviour
{

    public List<Character> enemies;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "player") {
            foreach (Character chara in enemies) {
                chara.currentHealth = chara.getMaxHealth();
                CombatManager.i.characters.Add(chara);
            }
            SceneManager.LoadSceneAsync("Scenes/CombatScene", LoadSceneMode.Single);
        }
    }
}
