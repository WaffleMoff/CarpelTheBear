using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactRingScript : MonoBehaviour
{
    public Vector3 size_vector;
    public float size_adder;
    public float size_timer;
    public float size_time;
    // Start is called before the first frame update
    void Start()
    {
        size_adder = 6.4f;
        size_timer = 0.0f;
        size_time = 0.25f;
        size_vector = new Vector3(0.2f, 0.2f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = size_vector;
        if (size_timer < size_time) {
            size_timer += Time.deltaTime;
            size_vector.x += Time.deltaTime * size_adder;
            size_vector.y += Time.deltaTime * size_adder;
        } else {
            Destroy(gameObject);
        }    
    }
}

