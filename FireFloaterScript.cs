using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFloaterScript : MonoBehaviour
{
    public Rigidbody2D body;
    public LogicScript logic;
    public int base_score_ins;
    public int score_ins;
    public PlayerScript player_script;

    public HealthBarScript health;

    public GameObject ImpactRing;

    public GameObject FireRingSpawner;
    //self destruct
    private float sd_timer;
    private float sd_time;
    

    public float x_pos;
    public float y_pos;
    public float x_speed;
    public float y_speed;
    public float hover_time;
    public float hover_timer;
    public float deadzone;
    public Vector3 pos_vector;
    public Vector3 size_vector;
    public float scaler;
    void Start()
    {
        gameObject.name = "FireFloater";
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

        x_pos = transform.position.x;
        y_pos = transform.position.y;
        //x_speed = -Random.Range(1.5f, 2.5f);
        y_speed = Random.Range(1.5f, 2.5f);
        //hover_time = Random.Range(2.0f, 4.0f);
        hover_time = 0.6f;
        hover_timer = 0.0f;
        deadzone = -50.0f;
        pos_vector = transform.position;
        scaler = Random.Range(0.45f, 0.8f);
        x_speed = -(5.5f - scaler);
        if (x_pos < 0) {
            x_speed *= -1;
        }
        
        //transform.localScale = size_vector;
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

        transform.position = pos_vector;
        pos_vector.x += x_speed * Time.deltaTime;
        pos_vector.y += y_speed * Time.deltaTime;

        if (hover_timer > hover_time) {
            hover_timer = 0.0f;
            y_speed *= -1;
        } else {
            hover_timer += Time.deltaTime;
        }

        if (transform.position.x < deadzone || transform.position.x > (-1)*deadzone) {
            del_game_obj();
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.tag == "Detector") {
            Instantiate(FireRingSpawner, transform.position, transform.rotation);
            del_game_obj();
        }

        if (collision.gameObject.name == "Player") {
            Instantiate(FireRingSpawner, transform.position, transform.rotation);
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
