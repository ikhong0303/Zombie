using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject player;
    public Animator zombieAnimator;

    public float timer;
    public Collider zombieHand;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            navMeshAgent.isStopped = true;
            return;
        }

        navMeshAgent.SetDestination(player.transform.position);

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            zombieAnimator.SetBool("IsAttack", true);
            navMeshAgent.isStopped = true;
        }

        else if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            zombieAnimator.SetBool("IsAttack", false);
            navMeshAgent.isStopped = false;
        }

        AttackDealy();
    }

    void AttackDealy()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            zombieHand.enabled = false;
        }

        else if (timer <= 0)
        {
            zombieHand.enabled = true;
        }
    }

    public void Death()
    {
        zombieAnimator.SetTrigger("Death");
        isDead = true;
    }
}
