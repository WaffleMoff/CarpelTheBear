using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    public Vector3 size_vector;
    public float bar_initial_size;

    public float beginning_player_health;
    public float player_health;
    public float health_decrease_rate;
    public float base_health_decrease_rate;
    public float time_passed;

    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        beginning_player_health = 100.0f;
        player_health = beginning_player_health;
        base_health_decrease_rate = 20.0f;

        health_decrease_rate = base_health_decrease_rate;

        //the health bar is 8 units long, health is 100
        bar_initial_size = 8.0f;
        size_vector = new Vector3(bar_initial_size, 1.0f, 1.0f);

        //logic
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //TESTING PURPOSES INVULNERABILITY
        //player_health = 100.0f;

        //bar scales to percentage of health left
        size_vector.x = bar_initial_size * player_health / beginning_player_health;
        transform.localScale = size_vector;

        if (player_health > 0.0f) {
            //player health goes down
            player_health -= health_decrease_rate * Time.deltaTime;
            time_passed += Time.deltaTime;

        } else if (player_health <= 0.0f) {
            //call game over
            logic.game_over();

        }

    }

    //function to reset health
    public void reset_health_bar() {
        player_health = 100.0f; 
    }
    public void incr_dmg_rate() {
        health_decrease_rate += 2.5f;
    }
    public void dmg_player(float dmg) {
        player_health -= dmg;
    }


}
