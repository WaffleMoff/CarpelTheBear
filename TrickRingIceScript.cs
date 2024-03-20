using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickRingIceScript : MonoBehaviour
{
    public Rigidbody2D body;
    public LogicScript logic;
    public int base_score_ins;
    public int score_ins;
    public PlayerScript player_script;

    public HealthBarScript health;

    public GameObject ImpactRing;

    public GameObject IceRingRevealed;
    public GameObject RingFacade;

    //self destruct
    private float sd_timer;
    private float sd_time;
    
    void Start()
    {
        gameObject.name = "TrickRingIce";
        body.gravityScale = 0;
        base_score_ins = 1;
        score_ins = base_score_ins;

        //logic script
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();

        //health script
        health = GameObject.FindWithTag("Health").GetComponent<HealthBarScript>();

        //player script
        player_script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();

        //self destruct
        sd_timer = 0.0f;
        sd_time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (sd_timer < sd_time) {
            sd_timer += Time.deltaTime;
        } else if (sd_timer != 0.0f) {
            del_game_obj();
        } else {
            //do nothing
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.tag == "Detector") {
            IceRingRevealed.SetActive(true);
            RingFacade.SetActive(false);
            enter_self_destruct();
        }

        if (collision.gameObject.name == "Player") {
            player_script.enter_ice(0.75f);
            //impact ring
            Instantiate(ImpactRing, transform.position, transform.rotation);
            del_game_obj();
        } 
    }

    private void enter_self_destruct() {
        sd_time = 1.25f;
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
