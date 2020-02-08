using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    //변수 지정
    public GameObject WayPoint0;
    public GameObject WayPoint1;
    public float MoveSpeed = 3.0f;
    public float RotationSpeed = 2.0f;
    bool isWayPoint = false;
    bool isSearch = false;
    public GameObject Target; //New Challenger!!



    void Start()
    {

        GetComponent<Animation>().Play("idle");

    }



    void Update()
    {

        //탐색하다 걸리면 공격, 아니면 평소대로 패트롤

        if (isSearch == true)
        { //기존과 탐색 조건을 추가함

            Attack(); //공격기능			

        }

        else
        {

            GetComponent<Animation>().Play("run");

            WayPointMove(); //패트롤 기능

        }

    }



    void WayPointMove()
    {

        if (isWayPoint == false)
        {

            //회전

            transform.rotation =

                Quaternion.Slerp(transform.rotation,

                 Quaternion.LookRotation(WayPoint1.transform.position - transform.position), 1);

            //이동

            transform.Translate(Vector3.forward * Time.smoothDeltaTime * MoveSpeed);

            //반전

            if (Vector3.Distance(transform.position, WayPoint1.transform.position) <= 0.5f)

                isWayPoint = true;

        }

        else
        {

            //회전

            transform.rotation =

                Quaternion.Slerp(transform.rotation,

                 Quaternion.LookRotation(WayPoint0.transform.position - transform.position), 1);

            //이동

            transform.Translate(Vector3.forward * Time.smoothDeltaTime * MoveSpeed);

            //반전

            if (Vector3.Distance(transform.position, WayPoint0.transform.position) <= 0.5f)

                isWayPoint = false;

        }



        Search();//탐색기능 <-New Challenger!!

    }



    void Search()
    {

        float distance = Vector3.Distance(Target.transform.position, transform.position);

        //거리가 가까워지면 탐색에 걸림

        if (distance <= 10)

            isSearch = true;

    }



    void Attack()
    {

        GetComponent<Animation>().CrossFade("shoot", 0.5f); //애니메이션 변경

        //돌아보는 방향을 플레이어 쪽으로

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), Time.smoothDeltaTime * 5.0f);

        float distance = Vector3.Distance(Target.transform.position, transform.position);

        //거리가 멀어지면 탐색 실패

        if (distance > 10)

            isSearch = false;

    }

}



