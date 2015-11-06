using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public List<Vector3> EnemyPath;
    public GameObject Spawn;
    public GameObject End;
    private static GameController _instance;

	// Use this for initialization
	void Start ()
	{
	    if (_instance != null)
	        return;
	    _instance = this;
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.S))
	    {
	        var zombie = new Zombie();
            zombie.Summon();
	    }
	}

    public static GameController GetInstance()
    {
        return _instance;
    }


}
