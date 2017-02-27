using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour 
{

	public float offsetX = -6;
	public float offsetY = 8;

	public string[] fileNames;
	public const char KEY_WALL = '*';
	public const char KEY_PLAYER = 'P';
	public const char KEY_GOAL = 'G';
	public const string THIS_SCENE = "Week5";
	public const string LEVEL_PATH = "/Resources/Levels/";
	public const string FILE_TYPE = ".txt";
	public const string LEVEL_HOLDER = "Level Holder";
	
	public static int levelNum = 0;

	private bool playerHasBeenLoaded;
	private bool goalHasBeenLoaded;

	// Use this for initialization
	void Start () 
	{
		playerHasBeenLoaded = false;
		string fileName = fileNames[levelNum];

		string filePath = Application.dataPath + LEVEL_PATH + fileName + FILE_TYPE;

		StreamReader sr = new StreamReader(filePath);

		GameObject levelHolder = new GameObject(LEVEL_HOLDER);

		int yPos = 0;

		

		while(!sr.EndOfStream)
		{
			string line = sr.ReadLine();

			for(int xPos = 0; xPos < line.Length; xPos++)
			{

				if(line[xPos] == KEY_WALL)
				{
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

					cube.transform.parent = levelHolder.transform;

					cube.transform.position = new Vector3( xPos + offsetX, yPos + offsetY, 0);
					cube.AddComponent<Rigidbody>();
					cube.GetComponent<Rigidbody>().useGravity = false;
					cube.GetComponent<Rigidbody>().isKinematic = true;
				}

				if(line[xPos] == KEY_PLAYER && !playerHasBeenLoaded)
				{
					GameObject player = Instantiate(Resources.Load("Prefabs/Player") as GameObject);
					player.transform.position = new Vector3 (xPos + offsetX, yPos + offsetY, 0);
					playerHasBeenLoaded = true;
				}

				if(line[xPos] == KEY_GOAL && !goalHasBeenLoaded)
				{
					GameObject goal = Instantiate(Resources.Load("Prefabs/Goal") as GameObject);
					goal.transform.position = new Vector3 (xPos + offsetX, yPos + offsetY, 0);
					goal.transform.parent = levelHolder.transform;
					goalHasBeenLoaded = true;
				}
			}
			yPos--;
		}

		sr.Close();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			levelNum++;
			SceneManager.LoadScene(THIS_SCENE);
		}
	}
}
