using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour 
{

	public float rotationSpeed = 2.0f;
	private float zRotation;
	private GameObject level;

	public const string SCROLLWHEEL = "Mouse ScrollWheel";
	public const string LEVEL_HOLDER = "Level Holder";
	// Use this for initialization
	void Start () 
	{
		
	}
	
	void RotateLevel(float z)
	{
		level.transform.localRotation = Quaternion.Euler(level.transform.rotation.x, level.transform.rotation.y, z * rotationSpeed);
	}

	// Update is called once per frame
	void Update () 
	{
		if (level == null)
		{
			level = GameObject.Find(LEVEL_HOLDER);
		}

		zRotation = zRotation + Input.GetAxis(SCROLLWHEEL);
		RotateLevel(zRotation);
	}
}
