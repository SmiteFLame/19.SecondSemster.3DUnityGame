using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    int speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fMove = speed;
        transform.Translate(Vector3.forward * fMove);
        Destroy(this.gameObject, 3.0f);
        
    }
}
