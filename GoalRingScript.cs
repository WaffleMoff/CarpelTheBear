using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRingScript : MonoBehaviour
{
    public Rigidbody2D body;
    public LogicScript logic;
    public int base_score_ins;
    public int score_ins;
    public PlayerScript player_script;

    public HealthBarScript health;

    public GameObject ImpactRing;
    
    void Start()
    {
        gameObject.name = "GoalRing";
        body.gravityScale = 0;
        base_score_ins = 1;
        score_ins = base_score_ins;


        //logic script
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();

        //health script
        health = GameObject.FindWithTag("Health").GetComponent<HealthBarScript>();

        //player script
        player_script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            if (player_script.check_sprint_active()) {
                logic.add_combo();
                logic.add_score(1 + logic.get_combo());
                logic.spawn_gr_close_to_player();
            } else {
                logic.reset_combo();
                logic.add_score(score_ins);
                logic.spawn_gr();
            }
            //logic and health
            health.reset_health_bar();
            logic.incr_rings_collected();

            //impact ring
            Instantiate(ImpactRing, transform.position, transform.rotation);

            del_game_obj();
        }        
    }
    public void del_game_obj() {
        Destroy(gameObject);
    }
    public float get_x_pos() {
        return transform.position.x;
    }
    public float get_y_pos() {
        return transform.position.y;
    }
    public void debug_check() {
        Debug.Log("Test");
    }
}
