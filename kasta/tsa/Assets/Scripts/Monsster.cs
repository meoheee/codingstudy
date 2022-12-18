using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monsster : MonoBehaviour
{
    public enum State
    {
        IDLE, TRACE, ATTACK, DIE
    }
    public State state = State.IDLE;
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;
    public bool isDie = false;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;

    private readonly int hashTrace = Animator.StringToHash("isTrace");
    private readonly int hashAttack = Animator.StringToHash("isAttack");
    private readonly int hashHit = Animator.StringToHash("hit");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");

    private int hp = 100;

    private GameObject bloodEffect;
    // Start is called before the first frame update
    void Awake()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");

        //agent.destination = playerTr.position;

    }
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            if (state == State.DIE) yield break;
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
                case State.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;
                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);

                    break;
                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;
                case State.DIE:
                    isDie = true;
                    agent.isStopped = true;
                    anim.SetTrigger(hashDie);
                    GetComponent<CapsuleCollider>().enabled = false;

                    yield return new WaitForSeconds(3.0f);

                    hp = 100;
                    isDie = false;
                    GetComponent<CapsuleCollider>().enabled = true;
                    this.gameObject.SetActive(false);



                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            Destroy(coll.gameObject);
            anim.SetTrigger(hashHit);
            Vector3 pos = coll.GetContact(0).point;
            Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);
            showBloodEffect(pos, rot);
            hp -= 10;
            //Debug.Log(hp);
            if (hp <= 0)
            {
                state = State.DIE;
            }
        }
    }

    void showBloodEffect(Vector3 pos, Quaternion rot)
    {
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot, monsterTr);
        Destroy(blood, 1.0f);
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
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }
    void OnPlayerDie()
    {
        StopAllCoroutines();
        agent.isStopped = true;
        anim.SetFloat(hashSpeed, Random.Range(0.8f, 1.2f));
        anim.SetTrigger(hashPlayerDie);
    }
    void OnEnable()
    {
        Player.OnPlayerDie += this.OnPlayerDie;
        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }
    void OnDisable()
    {
        Player.OnPlayerDie -= this.OnPlayerDie;
    }
}