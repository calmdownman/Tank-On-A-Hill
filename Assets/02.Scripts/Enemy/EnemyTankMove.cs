using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMove : MonoBehaviour
{
    private Transform target;
    NavMeshAgent nav;
    Rigidbody rigid;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Tank").transform;
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }
    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);
    }
}
