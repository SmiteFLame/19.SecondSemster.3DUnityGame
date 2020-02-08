using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent_Move : MonoBehaviour
{
    float speed = 0.1f;
    Transform player; 
    Transform opponent; 
    const float RADAR_DIST = 40f; // 자동차 탐지 거리
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        opponent =GameObject.FindWithTag("MONSTER").transform;
    }

    // Update is called once per frame
    void Update()
    {
     
        
        float fMove = Time.deltaTime * speed;
        transform.Translate(Vector3.forward * speed);

        float dist = Vector3.Distance(player.position, transform.position);
        if (dist <= RADAR_DIST)
        {
            opponent.LookAt(player);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("맞았당");
            GameObject director = GameObject.Find("GameDirector");
            //director.GetComponent<GameDirector>().DecreaseHp();
        }
    }
   
}
