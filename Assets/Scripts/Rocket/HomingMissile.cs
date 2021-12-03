using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
	[Header("- - - References - - -")]
	public Transform target;
	private Rigidbody2D rb;

	[Header("- - - Variables - - -")]
	public float speed = 5f;
	public float rotateSpeed = 200f;
	[SerializeField] bool targetLocked = false;
	[SerializeField] bool canLock = true;
	[SerializeField] float rotateAmount;
	[SerializeField] float distance;
	[SerializeField] float lockRotateThreshold;
	[SerializeField] float lockDistanceThreshold;
	[SerializeField] float slowTime;
	[SerializeField] float speedMultiplier;
	private bool inSlow = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (canLock && !targetLocked && !inSlow && distance < lockDistanceThreshold && rotateAmount < lockRotateThreshold) 
        {
			targetLock();
			StartCoroutine(slowSpeed());
		}
		Vector2 direction = (Vector2)target.position - rb.position;

		direction.Normalize();

        if (!targetLocked)
        {
			rotateAmount = Vector3.Cross(direction, transform.up).z;
			rb.angularVelocity = -rotateAmount * rotateSpeed;
			
		}

		rb.velocity = transform.up * speed;
	}



	void OnTriggerEnter2D()
	{
		//particle effect 
		//Destroy(gameObject);
	}
	void targetLock()
    {
		
		targetLocked = true;
		rb.angularVelocity = 0f;
		//Debug.DrawLine(transform.position, target.transform.position, Color.cyan, 3f);
		//speed *= 1.5f;
		//rb.velocity = transform.up * speed;
		
	}
	IEnumerator slowSpeed()
    {
		inSlow = true;
		var oldspeed = speed;
		speed *= 0.5f;
		yield return new WaitForSeconds(slowTime);
		//targetLocked = true;
		speed = oldspeed * speedMultiplier;

	}

	public bool get_targetLocked()
    {
		return targetLocked;
    }
}