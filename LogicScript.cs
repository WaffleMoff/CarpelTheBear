using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    //score
    public int player_score;
    public Text scoreText;

    //health
    //public float player_health;

    //player object
    public GameObject player;
    public PlayerScript player_script;

    //score functions
    public void add_score(int score_to_add) {
        player_score += score_to_add;
        scoreText.text = player_score.ToString();
    }
    //gameover screen
    public GameObject game_over_screen;
    public bool is_game_over;

    public void game_over() {
        player.SetActive(false);
        game_over_screen.SetActive(true);
        is_game_over = true;
    }

    public void restart_game() {
        //when using buttons, you will have to go in unity and add an event to the button.
        //Then drop the game object with this script (not the script) in the event in the button, then select a function
        //for it to perform on click. In this case, we want to select this function
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //spawning
    public float spawn_rate;
    public float spawn_timer;
    public float spawn_time;
    public float top_bound;
    public float bottom_bound;
    public float left_right_bound;
    public GameObject GoalRing;
    public GameObject TrickRingIce;
    public GameObject TrickRingFire;
    public GameObject FireFloater;
    public float reaction_radius;
    public float trick_requirement;

    public void spawn_gr() {
        Instantiate(GoalRing, new Vector3(Random.Range(-left_right_bound, left_right_bound), Random.Range(bottom_bound, top_bound), 0), transform.rotation);
        //Instantiate(TrickRingIce, new Vector3(Random.Range(-left_right_bound, left_right_bound), Random.Range(bottom_bound, top_bound), 0), transform.rotation);
    }

    public void spawn() {
        //Instantiate(GoalRing, new Vector3(Random.Range(-left_right_bound, left_right_bound), Random.Range(bottom_bound, top_bound), 0), transform.rotation);
        Instantiate(TrickRingIce, new Vector3(Random.Range(-left_right_bound, left_right_bound), Random.Range(bottom_bound, top_bound), 0), transform.rotation);
    }

    public void spawn_away_from_player() {
        //Instantiate(GoalRing, new Vector3(Random.Range(-left_right_bound, left_right_bound), Random.Range(bottom_bound, top_bound), 0), transform.rotation);
        //Instantiate(TrickRingIce, get_pos_away_to_player(reaction_radius), transform.rotation);
        Instantiate(FireFloater, get_floater_pos(), transform.rotation);
        float chance = Random.Range(0.0f, 1.0f);
        float trick_chance = Random.Range(0.0f, 1.0f);
        if (trick_chance > trick_requirement) {
            if (difficulty_level >= 2.0f) {
                if (chance > 0.5) {
                    Instantiate(TrickRingIce, get_pos_away_to_player(reaction_radius), transform.rotation);
                } else {
                    Instantiate(TrickRingFire, get_pos_away_to_player(reaction_radius), transform.rotation);
                }
            }
        }
    }

    public void spawn_gr_close_to_player() {
        Instantiate(GoalRing, get_pos_close_to_player(19.0f), transform.rotation);
    }

    public Vector3 get_pos_close_to_player(float radius_to_player) {
        Vector3 pos_close_to_player;
        pos_close_to_player = new Vector3(0.0f, 0.0f, 0.0f);
        float x_close_to_player;
        float y_close_to_player;
        float player_x = player_script.get_x_pos();
        float player_y = player_script.get_y_pos();
        x_close_to_player = (float)Random.Range(-radius_to_player, radius_to_player);
        y_close_to_player = (float)System.Math.Sqrt((radius_to_player*radius_to_player - x_close_to_player*x_close_to_player));
        if (Random.Range(0.0f, 1.0f) > 0.5f) {
            y_close_to_player *= -1;
        }
        x_close_to_player += player_x;
        y_close_to_player += player_y;

        //check if it is out of bounds
        if (x_close_to_player >= left_right_bound || x_close_to_player <= - left_right_bound || y_close_to_player >= top_bound || y_close_to_player <= bottom_bound) {
            return get_pos_close_to_player(radius_to_player);
        } else {
            pos_close_to_player.x = x_close_to_player;
            pos_close_to_player.y = y_close_to_player;
            return pos_close_to_player;
        }
    }

    public Vector3 get_pos_away_to_player(float radius_to_player) {
        Vector3 pos_away_from_player;
        pos_away_from_player = new Vector3(Random.Range(-left_right_bound, left_right_bound), Random.Range(bottom_bound, top_bound), 0);
        float x = pos_away_from_player.x;
        float y = pos_away_from_player.y;
        float player_x = player_script.get_x_pos();
        float player_y = player_script.get_y_pos();
        x -= player_x;
        y -= player_y;
        if (x*x + y*y > radius_to_player*radius_to_player) {
            return pos_away_from_player;
        } else {
            return get_pos_away_to_player(radius_to_player);
        }
    }

    public Vector3 get_floater_pos() {
        Vector3 floater_pos;
        float x = 47.0f;
        float y = Random.Range(bottom_bound, top_bound);
        float chance = Random.Range(0.0f, 1.0f);
        if (chance > 0.5) {
            x *= -1;
        }
        floater_pos.x = x;
        floater_pos.y = y;
        floater_pos.z = 0;
        return floater_pos;


    }

    //difficulty
    public int rings_collected;
    public float difficulty_level;
    public GameObject DifficultyIndicator;
    //difficulty level will go from 0.0 to 1.0 and then beyond
    //the goods will spawn 100% of the time and at the spawn rate
    //the bads will spawn with likelyhood = (difficulty level) ^ 3 / difficulty level at the spawn rate
    //the spawn rate will increase over time (unsure what model to use)

    public void incr_rings_collected() {
        rings_collected ++;
        if (rings_collected % 20 == 0) {
            incr_diff_lvl();
        }
    }

    public void incr_diff_lvl() {
        Instantiate(DifficultyIndicator, new Vector3(0.0f, -28.3f, 0.0f), transform.rotation);
        Instantiate(DifficultyIndicator, new Vector3(0.0f, 26.3f, 0.0f), transform.rotation);
        //add a max difficulty??
        difficulty_level += 1.0f;
        if (difficulty_level > 2.0f) {
            trick_requirement -= 0.075f;
        }  

        //randomly choose negative effect
        health.incr_dmg_rate();
        
        if (reaction_radius > 0.5f){
            reaction_radius -= 0.5f;
        }
        //decrease reaction radius
        //increase bad stuff spawn rate
        if (spawn_rate > 0.75f) {
            spawn_rate -= 0.1f;
        }
    }

    //combo
    public int combo;

    public void add_combo() {
        combo += 1;
    }
    public int get_combo() {
        return combo;
    }
    public void reset_combo() {
        combo = 0;
    }

    //healthbar
    HealthBarScript health;
    


    // Start is called before the first frame update
    void Start()
    {
        //resolution
        Screen.SetResolution(1920, 1080, true);
        
        //UI
        player_score = 0;
        //player_health = 100.0f;
        //game over?
        is_game_over = false;
        //spawning
        spawn_rate = 3.0f;
        spawn_timer = 0.0f;
        top_bound = 18.0f;
        bottom_bound = -21.0f;
        left_right_bound = 37.5f;
        reaction_radius = 24.0f;

        //difficulty
        difficulty_level = 0.0f;
        rings_collected = 0;

        //combo
        combo = 0;

        //player
        player_script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();

        //healthbar
        health = GameObject.FindWithTag("Health").GetComponent<HealthBarScript>();

        trick_requirement = 0.9f;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_game_over) {
            if (spawn_timer < spawn_rate) {
                spawn_timer += Time.deltaTime;
            } else {
                spawn_timer = 0.0f;
                //abstract with spawning function
                //spawn();
                spawn_away_from_player();
                //spawn_close_to_player();
                //maybe we don't spawn goalrings with this

                //should only increment like max 22ish times a game
                //health.incr_dmg_rate();
            }
        }
        if (Input.GetKey("escape")) {
            Debug.Log("EXIT");
            Application.Quit();
        }
    }

}
