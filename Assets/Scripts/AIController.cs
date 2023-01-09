using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    // This script is the enemy, it aimed to follow the player when he is further than 5 units
    // It will stop when the player is closer than 5 units
    // The enemy will not target the player if he is not in the field of view of the enemy
    // The enemy will not target the player if he is behind a wall

    NavMeshAgent agent;
    private Animator _animator;
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float _distanceToTarget = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = 800f;
        agent.acceleration = 60f;
        agent.speed = 15f;
        agent.stoppingDistance = 0.5f;

        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > _distanceToTarget)
        {
            Vector3 direction = _player.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, int.MaxValue))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.DrawLine(transform.position + transform.up, hit.point, Color.red);
                    agent.SetDestination(_player.transform.position);
                }
            }
        }
        else
        {
            agent.ResetPath();
        }
        _animator.SetFloat("HorizontalSpeed", agent.velocity.magnitude / agent.speed);
    }
}
