using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1);
        Destroy(this.gameObject, 15);
    }
  
}
