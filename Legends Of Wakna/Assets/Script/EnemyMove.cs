using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour
{
	[SerializeField] Transform player;
	private NavMeshAgent navMeshAgent;
	private Animator anim;
	private EnemyHealth enemyHealth;

	private void Awake()
	{
		Assert.IsNotNull(player);
	}
	// Start is called before the first frame update
	void Start()
    {
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
