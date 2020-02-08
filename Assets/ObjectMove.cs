using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{

    public float Speed = 4.0f;
    public GameObject Target;
    int randomNuber;
    private Animator anim;
    public int hashAttack = Animator.StringToHash("isAttack");
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        randomNuber = Random.Range(1, 4);
      
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Attack());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("맞았당");
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp1();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          
            Debug.Log("맞았당");
            //anim.SetBool(hashAttack, true);
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp1();
           
           
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("화살이나 칼 맞음");
           // Destroy(gameObject);
            //GameObject director = GameObject.Find("GameDirector");
            //director.GetComponent<GameDirector>().DecreaseHp1();
        }
    }
    IEnumerator Attack()
    {
        anim.SetBool(hashAttack, true);
        yield return new WaitForSeconds(1.0f);
        //anim.SetBool(hashAttack, false);
     }


}
