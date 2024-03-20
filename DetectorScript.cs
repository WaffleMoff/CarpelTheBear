using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour
{
    public PlayerScript player_script;
    public Vector3 pos_vector;
    // Start is called before the first frame update
    void Start()
    {
        pos_vector = new Vector3(0.0f, 0.0f, 0.0f);
        player_script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos_vector;
        pos_vector.x = player_script.get_x_pos();
        pos_vector.y = player_script.get_y_pos();
    }
}
