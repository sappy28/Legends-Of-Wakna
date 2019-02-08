using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
	private GameObject player;
	private PlayerHealth playerHealth;

	private void Start()
	{
		player = GameManager.instance.Player;
		playerHealth = player.GetComponent<PlayerHealth>();
		GameManager.instance.RegisterPowerUps();
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == player)
		{
			playerHealth.powerUpHealth();
			Destroy(gameObject);
		}
		
	}
}
