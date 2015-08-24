using UnityEngine;
using System.Collections;

//When the object is clicked the rotation of the object changes to the direction of the click
// with the "source" object as the origin of the direction vector to the mouse.
public class ClickRotate : MonoBehaviour 
{
	public Vector3 targetRotation;
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
		rotationSpeed = 0.5f;
	}
	
	IEnumerator RotateCoroutine ()
	{
		rotateCoroutineRunning = true;
		float t = 0;
		
		while (t/10.0f <= 1)
		{
			t += Time.deltaTime;
			//transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, t/10.0f);
			yield return null;
		}
		
		rotateCoroutineRunning = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.Rotate(targetRotation.normalized * rotationSpeed);
		rotation += rotationSpeed;
		targetQuaternion = Quaternion.AngleAxis(rotation % 360, targetRotation);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime);
		if (rotateCoroutineRunning)
		{
			StopCoroutine("RotateCoroutine");
		}
		
		StartCoroutine(RotateCoroutine());
		
		//float newAngle = (targetRotation.w + 0.1f) % 360.0f;
		//targetRotation.w = newAngle;
		//gameObject.transform.rotation = targetRotation;//Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime*rotationSpeed);
		
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray);
			
			foreach (RaycastHit hit in hits)
			{
				if (hit.rigidbody.gameObject == gameObject)
				{
					Vector3 clickDir = hit.point - Camera.main.transform.position;
					Vector3 trudir = (clickDir.normalized - Camera.main.transform.forward.normalized);
					trudir.z = 0;
					Vector3 orth = Vector3.Cross(Camera.main.transform.forward, trudir.normalized);
					Debug.DrawRay(Camera.main.transform.position, trudir, Color.green, 5.0f);
					Debug.DrawRay(Camera.main.transform.position, orth, Color.blue, 5.0f);
					targetRotation = orth;
					
					//targetRotation.y = clickDir.x;
					//targetRotation.x = clickDir.y;
					
					/*
					Vector3 rotationVector = clickDir.normalized;
					//Quaternion newRotation = Quaternion.FromToRotation(Camera.main.transform.forward, hit.point);
					targetRotation.x = rotationVector.x;
					targetRotation.y = rotationVector.y;
					targetRotation.z = rotationVector.z;
					targetRotation.w = transform.rotation.w + 1;
					*/
				}
			}
	}
	
	public void Rotate (Vector3 angles)
	{
		transform.Rotate(angles.normalized * rotationSpeed);
	}
	
}
