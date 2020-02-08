using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_set_spped : MonoBehaviour
{
    float def_speed;
    // Start is called before the first frame update
    void Start()
    {
        def_speed = GameObject.Find(this.ToString()).GetComponent<TestAgentScript>().agent.speed;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
