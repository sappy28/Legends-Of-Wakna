using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float moveSpeed = 10.0f;
	[SerializeField] LayerMask layerMask;

	private CharacterController characterController;
	private Animator anim;
	private Vector3 currentLookAt = Vector3.zero;
	private BoxCollider[] swords;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		swords = GetComponentsInChildren<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.instance.GameOver)
		{
			Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			characterController.SimpleMove(moveSpeed * moveDirection);

			if (moveDirection == Vector3.zero)
			{
				anim.SetBool("IsWalking", false);
			}
			else
			{
				anim.SetBool("IsWalking", true);
			}

			if (Input.GetMouseButtonDown(0))
			{
				anim.Play("Attack");
			}
			else if (Input.GetMouseButtonDown(1))
			{
				anim.Play("Spin Attack");
			}
		}	
	}

	void FixedUpdate()
	{
		if (!GameManager.instance.GameOver)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);
			if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
			{
				if (hit.point != currentLookAt)
				{
					currentLookAt = hit.point;
				}
				Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
				Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);

			}
		}
	}

	void BeginAttack()
	{
		foreach (var weapons in swords)
			weapons.enabled = true;
	}

	void EndAttack()
	{
		foreach (var weapons in swords)
			weapons.enabled = false;
	}
}
