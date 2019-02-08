using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
	[SerializeField] private float range = 3f;
	[SerializeField] private float timeBetweenHit = 1f;
	[SerializeField] Transform fireLocation;

	private GameObject player;
	private Animator anim;
	private bool IsPlayerInRange;
	private EnemyHealth enemyHealth;
	private GameObject arrow;
	// Start is called before the first frame update
	void Start()
	{
		arrow = GameManager.instance.Arrow;
		enemyHealth = GetComponent<EnemyHealth>();
		player = GameManager.instance.Player;
		anim = GetComponent<Animator>();
		StartCoroutine(attack());
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
		{
			IsPlayerInRange = true;
			anim.SetBool("PlayerInRange", true);
			RotateTowards(player.transform);
		}
		else
			IsPlayerInRange = false;
		anim.SetBool("PlayerInRange", false);
	}

	IEnumerator attack()
	{
		if (IsPlayerInRange && !GameManager.instance.GameOver && enemyHealth.IsAlive)
		{
			anim.Play("Attack");
			yield return new WaitForSeconds(timeBetweenHit);
		}
		yield return null;
		StartCoroutine(attack());
	}

	private void RotateTowards(Transform player)
	{
		Vector3 direction = (player.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
	}

	public void FireArrow()
	{
		GameObject newArrow = Instantiate(arrow) as GameObject;
		newArrow.transform.position = fireLocation.position;
		newArrow.transform.rotation = transform.rotation;
		newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 25f;

	}
}
