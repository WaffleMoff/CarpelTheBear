using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyIndicatorScript : MonoBehaviour
{
    public GameObject FirePattern;
    public float dist_between;
    public Vector3 spawn_vec_right;
    public Vector3 spawn_vec_left;
    public int num_spawns;
    public int i;
    public bool initiated;
    // Start is called before the first frame update
    void Start()
    {
        dist_between = 2.0f;
        spawn_vec_left = transform.position;
        spawn_vec_left.x -= dist_between;
        spawn_vec_right = transform.position;
        spawn_vec_right.x += dist_between;
        num_spawns = 23;
        initiated = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!initiated) {
            initiate();
            initiated = true;
        } else {
            del_game_obj();
        }
    }

    void initiate() {
        i = 0;
        Instantiate(FirePattern, transform.position, transform.rotation);
        while (i < num_spawns) {
            Instantiate(FirePattern, spawn_vec_left, transform.rotation);
            Instantiate(FirePattern, spawn_vec_right, transform.rotation);
            spawn_vec_left.x -= dist_between;
            spawn_vec_right.x += dist_between;
            i++;
        }
    }

    public void del_game_obj() {
        Destroy(gameObject);
    }
}
