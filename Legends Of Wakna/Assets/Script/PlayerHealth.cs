using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private int startingHealth = 100;
	[SerializeField] float timeSinceLastHit = 2f;
	[SerializeField] Slider healthSlider;

	private int currentHealth;
	private CharacterController characterController;
	private Animator anim;
	private float timer = 0f;
	private AudioSource audio;
	private ParticleSystem humanBlood;

	private void Awake()
	{
		Assert.IsNotNull(healthSlider);
	}
	// Start is called before the first frame update
	void Start()
    {
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		currentHealth = startingHealth;
		humanBlood = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
    }

	void OnTriggerEnter(Collider other)
	{
		if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
		{
			if(other.tag == "Weapons")
			{
				takehit();
				humanBlood.Play();
				timer = 0;
			}
		}
	}

	void takehit()
	{
		if(currentHealth > 0)
		{
			GameManager.instance.PlayerHit(currentHealth);
			anim.Play("Hurt");
			audio.PlayOneShot(audio.clip);
			currentHealth -= 10;
			healthSlider.value = currentHealth;
		}
		else if (currentHealth <= 0)
		{
			killplayer();
		}
	}

	void killplayer()
	{
		GameManager.instance.PlayerHit(currentHealth);
		anim.SetTrigger("IsDie");
		characterController.enabled = false;
		audio.PlayOneShot(audio.clip);
	}
}
