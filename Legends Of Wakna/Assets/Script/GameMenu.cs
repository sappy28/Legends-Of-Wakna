using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
	[SerializeField] GameObject hero;
	[SerializeField] GameObject tanker;
	[SerializeField] GameObject soldier;
	[SerializeField] GameObject ranger;

	private Animator animHero;
	private Animator animTanker;
	private Animator animSoldier;
	private Animator animRanger;

	void Awake()
	{
		Assert.IsNotNull(hero);
		Assert.IsNotNull(tanker);
		Assert.IsNotNull(soldier);
		Assert.IsNotNull(ranger);
	}
	// Start is called before the first frame update
	void Start()
    {
		animHero = hero.GetComponent<Animator>();
		animTanker = tanker.GetComponent<Animator>();
		animSoldier = soldier.GetComponent<Animator>();
		animRanger = ranger.GetComponent<Animator>();

		StartCoroutine(MenuShow());
	}

	void Update()
	{
		
	}

	IEnumerator MenuShow()
	{
		yield return new WaitForSeconds(1f);
		animHero.Play("Spin Attack");
		yield return new WaitForSeconds(1f);
		animTanker.Play("Attack");
		yield return new WaitForSeconds(1f);
		animSoldier.Play("Attack");
		yield return new WaitForSeconds(1f);
		animRanger.Play("Attack");

		StartCoroutine(MenuShow());
	}

	public void Battle()
	{
		SceneManager.LoadScene("Level");
	}
	public void Quit()
	{
		Application.Quit();
	}
}
