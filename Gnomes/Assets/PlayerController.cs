using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MyMonoBehaviour 
{
	public float leashLength;
	public float moveSpeed;
	Vector3 originalPos;
	CharacterController playerController;
	Rotation rotateScript;
	
	// Use this for initialization
	void Start () 
	{
		originalPos = transform.position;
		originalPos.z = 0;
		leashLength = 0.5f;
		moveSpeed = 1.0f;
		playerController = memoizer.GetMemoizedComponent<CharacterController>(gameObject);
		rotateScript = memoizer.GetMemoizedComponent<Rotation>(GameObject.FindGameObjectWithTag("Sphere"));
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 moveDir = GetMoveDir();
		Vector3 currentPos = transform.position;
		currentPos.z = 0;
		//Debug.Log(moveDir);
		Debug.Log(currentPos);
		Debug.Log(originalPos);
		
		if (Vector3.Distance(currentPos + moveDir * Time.deltaTime, originalPos) < leashLength)
		{
			playerController.Move (moveDir * Time.deltaTime);
		}
		else
		{
				rotateScript.Rotate (moveDir);
		}
	}
	
	Vector3 GetMoveDir ()
	{
		//Prepare the mouse pointer position.
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Camera.main.nearClipPlane;
		
		//Find the mouse position in world space.
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
		mouseWorldPos.z = 0;
		
		//Get the direction of the movement vector from original position to current mouse position.
		return (mouseWorldPos - originalPos).normalized;
	}
}