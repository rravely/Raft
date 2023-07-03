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
    [SerializeField] Image bloodSplatterImage;

    NavMeshAgent agent;
    Animator sharkAni;

    [HideInInspector] public bool isFollow = false;
    [HideInInspector] public bool isAttacking = false;
    float attackBet = 5f;
    float attackTime = 0f;

    //Fade in
    float fadeInTime = 1f;
    float timeFadeIn = 0f;

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
            else
            {
                FollowPlayer(0.1f);
            }

            if (attackTime >= attackBet && (sharkAttackPoint.position - transform.position).sqrMagnitude <= 1.1f)
            {
                sharkAni.SetBool("Bite", true);
                isAttacking = true;
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
        playerState.health -= 10f;
        StartCoroutine(BloodSplatter_co());
    }

    IEnumerator BloodSplatter_co()
    {
        bloodSplatterImage.gameObject.SetActive(true);

        Color imgColor = bloodSplatterImage.color;
        imgColor.a = 1f;

        while (imgColor.a > 0f)
        {
            timeFadeIn += Time.deltaTime / fadeInTime;
            imgColor.a = Mathf.Lerp(1f, 0f, timeFadeIn);
            bloodSplatterImage.color = imgColor;
            yield return null;
        }

        bloodSplatterImage.gameObject.SetActive(false);
        fadeInTime = 1f;
        timeFadeIn = 0f;
    }
}
