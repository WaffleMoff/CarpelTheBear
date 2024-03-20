using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePatternScript : MonoBehaviour
{
    public float shift_timer;
    public float shift_time;
    public float height_change;
    public float width_change;
    public Vector3 control_transform;
    public float alive_timer;
    public float alive_time;
    public float stage;
    public float rot_prob;
    public float transport_speed;
    // Start is called before the first frame update
    void Start()
    {

        control_transform = transform.position;
        shift_timer = 0.0f;
        shift_time = Random.Range(0.15f, 0.35f); //0.25f;
        height_change = Random.Range(4.5f, 14.5f); //5.0f;
        //width_change = Random.Range(0.0f, 3.0f);
        width_change = 0.0f;
        alive_timer = 0.0f;
        alive_time = 0.25f;
        stage = 0.0f;
        //control_rotation = new Quaternion(0, 0, (Random.Range(0.0f, 45.0f)), 0.0f);
        rot_prob = Random.Range(0.0f, 1.0f);
        //if (rot_prob > 0.5f) {
            //transform.Rotate(0, 0, -22.5f, 0);
        //}
        transport_speed = 5.0f;
        if (transform.position.y > 10.0f) {
            transform.Rotate(0.0f, 0.0f, 180.0f);
            transport_speed *= -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = control_transform;
        //stage0
        if (stage < 1.0f) {
            control_transform.y += transport_speed * Time.deltaTime;
        } else if (stage < 2.0f) {
            control_transform.y -= transport_speed * Time.deltaTime;
        } else {
            
            if (alive_timer < alive_time) {
                alive_timer += Time.deltaTime;
            } else {
                del_game_obj();
            }
            
        }
        if (shift_timer < shift_time) {
            control_transform.y += height_change * Time.deltaTime;
            control_transform.x += width_change * Time.deltaTime;
            shift_timer += Time.deltaTime;
        } else {
            shift_timer = 0.0f;
            height_change *= -1;
            width_change *= -1;
        }
        stage += Time.deltaTime;        
    }
    public void del_game_obj() {
        Destroy(gameObject);
    }
}
