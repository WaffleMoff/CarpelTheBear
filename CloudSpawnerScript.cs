using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour
{
    public GameObject Cloud;
    public float range;
    public float spawn_time;
    public float spawn_timer;
    // Start is called before the first frame update
    void Start()
    {
        spawn_time = Random.Range(13.5f, 14.0f);
        range = 5.0f;
            Instantiate(Cloud, new Vector3(transform.position.x, 10 + Random.Range(-range, range), transform.position.z), transform.rotation);
            //Instantiate(Cloud, new Vector3(transform.position.x, 15 + Random.Range(-range, range), transform.position.z), transform.rotation);
            Instantiate(Cloud, new Vector3(transform.position.x, -10 + Random.Range(-range, range), transform.position.z), transform.rotation);
            //Instantiate(Cloud, new Vector3(transform.position.x, -15 + Random.Range(-range, range), transform.position.z), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn_timer > spawn_time) {
            spawn_timer = 0.0f;
            spawn_time = Random.Range(13.5f, 14.0f);
            Instantiate(Cloud, new Vector3(transform.position.x, 10 + Random.Range(-range, range), transform.position.z), transform.rotation);
            //Instantiate(Cloud, new Vector3(transform.position.x, 15 + Random.Range(-range, range), transform.position.z), transform.rotation);
            Instantiate(Cloud, new Vector3(transform.position.x, -10 + Random.Range(-range, range), transform.position.z), transform.rotation);
            //Instantiate(Cloud, new Vector3(transform.position.x, -15 + Random.Range(-range, range), transform.position.z), transform.rotation);
            //Instantiate(Cloud, new Vector3(transform.position.x, Random.Range(-range - 2, range - 3), transform.position.z), transform.rotation);
        } else {
            spawn_timer += Time.deltaTime;
        }
    }
}
