using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Shark : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] Transform sharkPatrol;
    [SerializeField] Transform sharkAttackPoint;

    [Header("Player")]
    [SerializeField] PlayerState playerState;

    [Header("Blood")]
    [SerializeField] BloodSplatter bloodSplatter;

    NavMeshAgent agent;
    Animator sharkAni;

    [HideInInspector] public bool isFollow = false;
    [HideInInspector] public bool isAttacking = false;
    bool afterAttack = false;
    float attackBet = 5f;
    float attackTime = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sharkAni = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!playerState.inWater && !playerState.inWaterSurface) //outside of water
        {
            isFollow = false;

            agent.destination = sharkPatrol.position;
            agent.speed = 1f;
            transform.position = new Vector3(transform.position.x, sharkPatrol.position.y, transform.position.z);
        }
        else
        {
            isFollow = true;
            
            if (isAttacking)
            {
                FollowPlayer(2f);
            }
            else if (!isAttacking && !afterAttack)
            {
                FollowPlayer(0.1f);
            }

            //Attack
            if (attackTime >= attackBet && (sharkAttackPoint.position - transform.position).sqrMagnitude <= 1.1f)
            {
                sharkAni.SetBool("Bite", true);
                isAttacking = true;
                afterAttack = true;
                attackTime = 0f;
            }
            else if (attackTime >= attackBet)
            {
                FollowPlayer(1f);
            }
            attackTime += Time.deltaTime;
        }
    }

    public void FollowPlayer(float speed)
    {
        agent.destination = sharkAttackPoint.position;
        agent.speed = speed;
        if (playerState.inWater)
        {
            transform.position = new Vector3(transform.position.x, playerState.transform.position.y + 0.1f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, -0.568f, transform.position.z);
        }
    }

    public void AttackPlayer()
    {
        if (playerState.inWater || playerState.inWaterSurface)
        {
            playerState.health -= 10f;
            bloodSplatter.BloodSplat();
            StartCoroutine(FollowPatrolTemp_co());
        }
    }

    IEnumerator FollowPatrolTemp_co()
    {
        agent.destination = sharkPatrol.position;
        agent.speed = 1f;
        float time = 0f;
        while (time < 3f)
        {
            transform.position = new Vector3(transform.position.x, playerState.transform.position.y, transform.position.z);
            if (!playerState.inWater || !playerState.inWaterSurface)
            {
                break;
            }
            time += Time.deltaTime;
            yield return null;
        }
        afterAttack = false;
    }
}
