using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    //base
    public Rigidbody2D body;

    //basic movement
    public Vector2 control_velocity;
    public float active_movement_rate;
    public float base_movement_rate;

    //sprint
    public float sprint_movement_rate;
    private float sprint_timer;
    private float sprint_time;
    
    //sprint blur
    public GameObject SprintBlur;

    //ice
    public float ice_movement_rate;
    private float ice_timer;
    private float ice_time;
    
    //sprint effect
    public GameObject IceEffect;

    //logic script
    public LogicScript logic;

    //stop time
    public float stop_time;
    public float stop_timer;

    //animation
    public Animator animator;
    public int dir_anim;
    public bool flipped;

    // Start is called before the first frame update
    void Start()
    {

        //base
        gameObject.name = "Player";
        body.gravityScale = 0;

        //basic movement
        control_velocity = new Vector2(0.0f, 0.0f);
        base_movement_rate = 30.0f * 0.7f;
        active_movement_rate = base_movement_rate;

        //sprint
        sprint_movement_rate = 50.0f * 0.7f;
        sprint_timer = 0.0f;
        sprint_time = 0.0f;
    
        //ice
        ice_movement_rate = 17.0f * 0.7f;
        ice_timer = 0.0f;
        ice_time = 0.0f;

        //logic script
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();

        //stop time
        stop_time = 0.3f;
        stop_timer = 0.0f;

        //animation
        dir_anim = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //animation
        dir_anim = 0;

        //basic movement
        body.velocity = control_velocity;

        //basic movement
        if (Input.GetKey(KeyCode.W)) {
            control_velocity.y = active_movement_rate;
            dir_anim = 1;
        } else if (control_velocity.y > 0){
            control_velocity.y = 0;
        }
        if (Input.GetKey(KeyCode.A)) {
            control_velocity.x = -active_movement_rate;
            dir_anim = 7;
        } else if (control_velocity.x < 0){
            control_velocity.x = 0;
        }
        if (Input.GetKey(KeyCode.S)) {
            control_velocity.y = -active_movement_rate;
            dir_anim = 5;
        } else if (control_velocity.y < 0){
            control_velocity.y = 0;
        }
        if (Input.GetKey(KeyCode.D)) {
            control_velocity.x = active_movement_rate;
            dir_anim = 3;
        } else if (control_velocity.x > 0){
            control_velocity.x = 0;
        }

        //double movement key inputs and correcting:
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
            control_velocity.y = (float)((active_movement_rate / 2) * System.Math.Sqrt(2));
            control_velocity.x = (float)((active_movement_rate / 2) * System.Math.Sqrt(2));
            dir_anim = 2;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
            control_velocity.y = (float)((active_movement_rate / 2) * System.Math.Sqrt(2));
            control_velocity.x = (float)(-(active_movement_rate / 2) * System.Math.Sqrt(2));
            dir_anim = 8;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
            control_velocity.y = (float)(-(active_movement_rate / 2) * System.Math.Sqrt(2));
            control_velocity.x = (float)(-(active_movement_rate / 2) * System.Math.Sqrt(2));
            dir_anim = 6;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
            control_velocity.y = (float)(-(active_movement_rate / 2) * System.Math.Sqrt(2));
            control_velocity.x = (float)((active_movement_rate / 2) * System.Math.Sqrt(2));
            dir_anim = 4;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            control_velocity.x = 0;
            control_velocity.y = 0;
            dir_anim = 0;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) {
            control_velocity.x = 0;
            control_velocity.y = 0;
            dir_anim = 0;
        }

        //stop time
        if (control_velocity.x == 0 && control_velocity.y == 0) {
            stop_timer += Time.deltaTime;
            if (stop_timer >= stop_time) {
                exit_sprint();
            }
        } else {
            stop_timer = 0.0f;
        }

        //sprint
        if (sprint_timer < sprint_time) {
            sprint_timer += Time.deltaTime;
        } else if (sprint_timer != 0.0f) {
            exit_sprint();
        } else {
            //do nothing
        }
        //ice
        if (ice_timer < ice_time) {
            ice_timer += Time.deltaTime;
        } else if (ice_timer != 0.0f) {
            exit_ice();
        } else {
            //do nothing
        }

        //animation
        animator.SetInteger("Dir", dir_anim);
    }

    //collision
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "GoalRing") {
            enter_sprint(1.75f);
        }        
    }

    //sprint
    public void enter_sprint(float input_sprint_time) {
        exit_ice();
        SprintBlur.SetActive(true);
        sprint_timer = 0.0f;
        active_movement_rate = sprint_movement_rate;
        if (input_sprint_time > (sprint_time - sprint_timer)) {
            sprint_time = input_sprint_time;
        } else {
            //do nothing
        } 
    }
    public void exit_sprint() {
        active_movement_rate = base_movement_rate;
        sprint_time = 0.0f;
        sprint_timer = 0.0f;
        SprintBlur.SetActive(false);
    }
    //ice
    public void enter_ice(float input_ice_time) {
        exit_sprint();
        IceEffect.SetActive(true);
        ice_timer = 0.0f;
        active_movement_rate = ice_movement_rate;
        if (input_ice_time > (ice_time - ice_timer)) {
            ice_time = input_ice_time;
        } else {
            //do nothing
        } 
    }
    public void exit_ice() {
        active_movement_rate = base_movement_rate;
        ice_time = 0.0f;
        ice_timer = 0.0f;
        IceEffect.SetActive(false);
    }

    public void reset_speed() {
        active_movement_rate = base_movement_rate;
    }

    public bool check_sprint_active() {
        if (active_movement_rate == sprint_movement_rate) {
            return true;
        } else {
            return false;
        }
    }

    //base functions
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
