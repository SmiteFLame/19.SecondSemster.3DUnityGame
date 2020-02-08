using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTest : MonoBehaviour
{
    private Animator anim;
    private int hashWalk = Animator.StringToHash("isWalk");
    public int hashAttack = Animator.StringToHash("isAttack");
    private int hashDeath = Animator.StringToHash("Anim_Death");
    private int hashHit = Animator.StringToHash("Anim_Hit");
    private int hashIdle = Animator.StringToHash("Anim_Idle");

    private float time_float;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //유한상태머신 시작
        StartCoroutine(FSM());
        //anim.SetBool(hashWalk, true);
        time_float = 0.0f;
        //StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        /*time_float += Time.deltaTime;
        if (time_float >= 3.0f)
        {
            Debug.Log("In");
            anim.SetTrigger(hashDeath);
            time_float = 0.0f;
        }*/
        
    }
    IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(0, 0, 1);
            yield return new WaitForSeconds(1.0f);
        }
    }
        IEnumerator FSM()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);


            anim.SetBool(hashWalk, false);
            yield return new WaitForSeconds(3.0f);
            anim.SetBool(hashWalk, true);

            anim.SetBool(hashAttack, true);
            yield return new WaitForSeconds(3.0f);
            anim.SetBool(hashAttack, false);

            anim.SetTrigger(hashHit);
            yield return new WaitForSeconds(3.0f);


            anim.SetTrigger(hashDeath);
            yield return new WaitForSeconds(3.0f);


            anim.SetTrigger(hashIdle);
        }
    }
}
