using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[SerializeField] private float range = 3f;
	[SerializeField] private float timeBetweenHit = 1f;

	private GameObject player;
	private Animator anim;
	private bool IsPlayerInRange;
	private BoxCollider[] weaponColliders;
	private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
		enemyHealth = GetComponent<EnemyHealth>();
		player = GameManager.instance.Player;
		anim = GetComponent<Animator>();
		weaponColliders = GetComponentsInChildren<BoxCollider>();
		StartCoroutine(attack());
	}

    // Update is called once per frame
    void Update()
    {
		if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
		{
			IsPlayerInRange = true;
		}
		else
			IsPlayerInRange = false;
    }

	IEnumerator attack()
	{
		if(IsPlayerInRange && !GameManager.instance.GameOver)
		{
			anim.Play("Attack");
			yield return new WaitForSeconds(timeBetweenHit);
		}
		yield return null;
		StartCoroutine(attack());
	}

	public void EnemyBeginsAttack()
	{
		foreach(var weapons in weaponColliders)
		{
			weapons.enabled = true;
		}
	}

	public void EnemyEndAttack()
	{
		foreach (var weapons in weaponColliders)
		{
			weapons.enabled = false;
		}
	}
}
