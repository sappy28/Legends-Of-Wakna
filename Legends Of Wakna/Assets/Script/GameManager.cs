using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	[SerializeField] GameObject player;
	[SerializeField] GameObject[] spawnPoints;
	[SerializeField] GameObject[] powerSpawn;
	[SerializeField] GameObject tanker;
	[SerializeField] GameObject soldier;
	[SerializeField] GameObject ranger;
	[SerializeField] GameObject arrow;
	[SerializeField] GameObject healthPowerUps;
	[SerializeField] GameObject speedPowerUps;
	[SerializeField] Text levelText;
	[SerializeField] Text endGameText;
	[SerializeField] int endLevel = 20;

	private bool gameOver = false;
	private int enemyKilled = 0;
	private int currentLevel;
	private float enemySpawnTime = 1;
	private float currentSpawnTime = 0;
	private GameObject newEnemy;
	private GameObject newPowerUps;
	private float powerSpawnTime = 5f;
	private float currentPowerSpawnTime = 0;
	private int powerUps = 0;

	private List<EnemyHealth> enemies = new List<EnemyHealth>();
	private List<EnemyHealth> enemiesKilled = new List<EnemyHealth>();

	public void RegisterEnemy(EnemyHealth enemy)
	{
		enemies.Add(enemy);
	}

	public void RegisterPowerUps()
	{
		powerUps++;
	}

	public void KilledEnemy(EnemyHealth enemy)
	{
		enemiesKilled.Add(enemy);
		enemyKilled++;
	}

	public bool GameOver
	{
		get { return gameOver; }
	}

	public GameObject Player
	{
		get { return player; }
	}

	public GameObject Arrow
	{
		get { return arrow; }
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	// Start is called before the first frame update
	void Start()
	{
		currentLevel = 1;
		StartCoroutine(spawn());
		StartCoroutine(powerUpSpawn());
		endGameText.GetComponent<Text>().enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		currentSpawnTime += Time.deltaTime;
		currentPowerSpawnTime += Time.deltaTime;
	}

	public void PlayerHit(int currentHP)
	{
		if (currentHP > 0)
		{ gameOver = false; }
		else
		{
			gameOver = true;
			StartCoroutine(endGame("Defeat"));
		}
	}

	IEnumerator spawn()
	{
		if (currentSpawnTime >= enemySpawnTime)
		{
			currentSpawnTime = 0;
			if (enemies.Count < currentLevel)
			{
				int randomNumber = Random.Range(0, spawnPoints.Length - 1);
				GameObject spawnLocation = spawnPoints[randomNumber];
				int randomEnemy = Random.Range(0, 3);
				if (randomEnemy == 0)
				{
					newEnemy = Instantiate(soldier) as GameObject;
				}
				else if (randomEnemy == 1)
				{
					newEnemy = Instantiate(ranger) as GameObject;
				}
				else if (randomEnemy == 2)
				{
					newEnemy = Instantiate(tanker) as GameObject;
				}
				newEnemy.transform.position = spawnLocation.transform.position;
			}
			if (enemiesKilled.Count == currentLevel && currentLevel != endLevel)
			{
				enemies.Clear();
				enemiesKilled.Clear();
				currentLevel++;
				levelText.text = "LEVEL " + currentLevel;
			}
			if(enemiesKilled.Count == endLevel)
			{
				StartCoroutine(endGame("Victory!"));
			}
		}
		yield return null;
		StartCoroutine(spawn());
	}

	IEnumerator powerUpSpawn()
	{
		if (currentPowerSpawnTime >= powerSpawnTime && enemyKilled % 10 == 9)
		{
			currentPowerSpawnTime = 0;
			int randomNumber = Random.Range(0, powerSpawn.Length - 1);
			GameObject spawnLocation = powerSpawn[randomNumber];
			newPowerUps = Instantiate(healthPowerUps) as GameObject;

			newPowerUps.transform.position = spawnLocation.transform.position;
		}
		yield return null;
		StartCoroutine(powerUpSpawn());
	}

	IEnumerator endGame(string Outcome)
	{
		endGameText.text = Outcome;
		endGameText.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("GameMenu");
	}
}
