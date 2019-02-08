using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int startingHealth = 20;
	[SerializeField] private float timeBetweenHit = 0.5f;
	[SerializeField] private float dissapearTime = 2f;

	private AudioSource audio;
	private Animator anim;
	private float timer = 0f;
	private NavMeshAgent nav;
	private bool isAlive;
	private Rigidbody rigidbody;
	private CapsuleCollider capsuleCollider;
	private bool dissapearEnemy = false;
	private int currentHealth;
	private ParticleSystem orcBlood;

	public bool IsAlive
	{
		get { return isAlive; }
	}

    // Start is called before the first frame update
    void Start()
    {
		GameManager.instance.RegisterEnemy(this);
		rigidbody = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		isAlive = true;
		currentHealth = startingHealth;
		orcBlood = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		if (dissapearEnemy)
		{
			transform.Translate(-Vector3.up * dissapearTime * Time.deltaTime);
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if (timer >= timeBetweenHit && !GameManager.instance.GameOver)
		{
			if(other.tag == "PlayerWeapon")
			{
				takeHit();
				orcBlood.Play();
				timer = 0f;
			}
		}
	}

	void takeHit()
	{
		if(currentHealth > 0)
		{
			currentHealth -= 10;
			anim.Play("Hurt");
			audio.PlayOneShot(audio.clip);
		}
		if(currentHealth <= 0)
		{
			isAlive = false;
			killEnemy();
		}
	}
	void killEnemy()
	{
		GameManager.instance.KilledEnemy(this);
		capsuleCollider.enabled = false;
		nav.enabled = false;
		anim.SetTrigger("EnemyDie");
		rigidbody.isKinematic = true;
		StartCoroutine(removeEnemy());
	}

	IEnumerator removeEnemy()
	{
		yield return new WaitForSeconds(4f);
		dissapearEnemy = true;
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}
}
