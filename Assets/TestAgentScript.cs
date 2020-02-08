using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TestAgentScript : MonoBehaviour
{
    public Transform Target;
    public float default_speed;
    public NavMeshAgent agent;
    int randomNuber;
    bool isSearch = false;
    private Animator anim;
    private int hashAttack = Animator.StringToHash("Anim_Attack");
    private int hashWalk = Animator.StringToHash("Anim_Walk");
    public bool check = true;
    public Transform bullet; // 총알
    public Transform spPoint; //발사 시점
    // Start is called before the first frame update



    public void stopit()
    {
        this.agent.speed = 0.0f;
    }

    public void moveup()
    {
        this.agent.speed = 5.5f;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        default_speed = agent.speed;
        randomNuber = Random.Range(1, 5);
        anim = GetComponent<Animator>();
        //if (randomNuber > 2)
        //{
        //    Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //}
        //else
        //{
            Target = GameObject.FindWithTag("Castle").GetComponent<Transform>();

        //}
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Target.position);
        if (isSearch == true && check)
        { //기존과 탐색 조건을 추가함

            StartCoroutine(Attack()); //공격기능         

        }
        Search();

    }

    void Search()
    {
        float distance = Vector3.Distance(Target.transform.position, transform.position);
        if (this.name == "Jack_A(Clone)")
        {
            if (distance <= 15)
            {
                isSearch = true;
            }
        }
        else
        {
            if (distance <= 3)
            {
                isSearch = true;
            }
        }

    }

    IEnumerator Attack()
    {
        check = false;

        //anim.SetBool(hashAttack, true);
        stopit();
        anim.SetTrigger(hashAttack);
        if (this.name == "Jack_A(Clone)")
        {
            Instantiate(bullet, spPoint.position, spPoint.rotation);
        }
        Debug.Log("적 공격");
        //돌아보는 방향을 플레이어 쪽으로
        yield return new WaitForSeconds(1.5f);
        //this.GetComponent<TestAgentScript>().moveup();
        GameObject director = GameObject.Find("GameDirector");
        director.GetComponent<GameDirector>().DecreaseHp1();
        check = true;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.smoothDeltaTime * 5.0f);
        Debug.Log(this.gameObject);
        float distance = Vector3.Distance(Target.transform.position, transform.position);
        //anim.SetBool(hashAttack, false);

        //거리가 멀어지면 탐색 실패

        if (distance > 3)
        {
            isSearch = false;
            anim.SetTrigger(hashWalk);
            moveup();
        }
    }
}