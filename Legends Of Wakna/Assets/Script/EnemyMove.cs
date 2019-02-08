using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMove : MonoBehaviour
{
	private Transform player;
	private NavMeshAgent navMeshAgent;
	private Animator anim;
	private EnemyHealth enemyHealth;

	// Start is called before the first frame update
	void Start()
    {
		player = GameManager.instance.Player.transform;
		enemyHealth = GetComponent<EnemyHealth>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update()
	{
		if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
		{
			navMeshAgent.SetDestination(player.position);
		}
		else if((GameManager.instance.GameOver || !GameManager.instance.GameOver) && !enemyHealth.IsAlive)
		{
			navMeshAgent.enabled = false;
		}
		else
		{
			navMeshAgent.enabled = false;
			anim.Play("Idle");
		}
	}
}
