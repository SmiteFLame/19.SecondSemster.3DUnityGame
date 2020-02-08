using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summon_enermy : MonoBehaviour
{
    public GameObject enermy;

    int speed = 10;
    bool check;
    void Start()
    {
        check= true;
    }
    void Update()
    {
        if (check)
        {
            StartCoroutine(WaitForIt());
        }
    }
    IEnumerator WaitForIt()
    {
        check = false;
        yield return new WaitForSeconds(2.0f);
        Instantiate<GameObject>(enermy, transform.position, Quaternion.identity);
        
    }
}
