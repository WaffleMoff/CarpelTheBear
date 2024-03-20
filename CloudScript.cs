using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        x_pos = transform.position.x;
        y_pos = transform.position.y;
        //x_speed = -Random.Range(1.5f, 2.5f);
        y_speed = Random.Range(1.0f, 1.1f);
        //hover_time = Random.Range(2.0f, 4.0f);
        hover_time = 0.0f;
        hover_timer = 0.0f;
        deadzone = -50.0f;
        pos_vector = transform.position;
        scaler = Random.Range(0.45f, 0.8f);
        x_speed = -(1.5f - scaler);
        size_vector = new Vector3(scaler, scaler, 1);
        transform.localScale = size_vector;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos_vector;
        pos_vector.x += x_speed * Time.deltaTime;
        pos_vector.y += y_speed * Time.deltaTime;
        if (hover_timer > hover_time) {
            hover_timer = 0.0f;
            y_speed *= -1;
        } else {
            hover_timer += Time.deltaTime;
        }

        if (transform.position.x < deadzone) {
            del_game_obj();
        }
    }
    public void del_game_obj() {
        Destroy(gameObject);
    }
}
