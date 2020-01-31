using UnityEngine;
using System.Collections;

public class AutoLevelLoader : MonoBehaviour
{

	public int levelID = 0;
	public float levelTime = 30;
	// Use this for initialization
	void Start ()
	{
		Invoke ("LoadLevel", levelTime);
	}
	
	void Update ()
	{
	
		if (Input.GetKeyDown (KeyCode.X)) {
			LoadLevel ();
		}
	}
	
	// Update is called once per frame
	void LoadLevel ()
	{
	
		AutoFade.LoadLevel (levelID, 3, 1, Color.black);
	
	}
}
