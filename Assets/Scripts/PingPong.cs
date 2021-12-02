using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField]
    float startingheight;
    [SerializeField]
    float maximumheight=0.5f;
    [SerializeField]
    float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, maximumheight) + startingheight, transform.position.z);
    }
}
