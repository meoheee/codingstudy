using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
using System.Runtime.CompilerServices;

public class Monsster : MonoBehaviour
{
    public enum State
    {
        IDLE, PATROL, TRACE, ATTACK, DIE
    }
    public State state = State.IDLE;
    public float traceDist = 10.0f;
    public float attackDist = 20.0f;
    public bool isDie = false;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //agent.destination = playerTr.position;
        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);
            if (distance < attackDist)
            {
                state = State.ATTACK;
            }
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
        
    }
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State. IDLE:
                    agent.isStopped = true;
                    anim.SetBool("isTrace", false);
                    break;
                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool("isTrace", true);
                    break;
                case State.ATTACK:
                    break;
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnDrawGizmos()
    {
        if (state == State.TRACE)
        {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, traceDist);
        }
        if (state == State.ATTACK)
        {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, traceDist);
        }
    }
}