using UnityEngine;
using System.Collections;

//When the object is clicked the rotation of the object changes to the direction of the click
// with the "source" object as the origin of the direction vector to the mouse.
public class Rotation : MonoBehaviour 
{
	Vector3 targetRotation;
	public float rotationSpeed;
	public bool rotateCoroutineRunning;
	float rotation;
	public Quaternion targetQuaternion;
	
	// Use this for initialization
	void Start ()
	{
		rotateCoroutineRunning = false;
		rotation = 0;
		targetQuaternion = Quaternion.identity;
		targetRotation = Vector3.zero;
		targetRotation.x = -1;
		targetRotation.y = 0;
		targetRotation.z = 0;
		rotationSpeed = 1.0f;
	}
	
	IEnumerator RotateCoroutine ()
	{
		rotateCoroutineRunning = true;
		float t = 0;
		
		while (t/10.0f <= 1)
		{
			t += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, t/10.0f);
			yield return null;
		}
		
		rotateCoroutineRunning = false;
	}

	// Update is called once per frame
	public void Rotate (Vector3 dir) 
	{
		targetRotation = new Vector3(-dir.y, dir.x, dir.z);
		transform.Rotate(targetRotation * rotationSpeed, Space.World);
	}
}
