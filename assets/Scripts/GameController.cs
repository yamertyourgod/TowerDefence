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
        GameObject.Find("Map").SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.Z))
	    {
	        var zombie = new Zombie();
            zombie.Summon();
	    }

        if (Input.GetKeyDown(KeyCode.M))
        {
            var minotaur = new Minotaur();
            minotaur.Summon();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var mage = new Mage();
            mage.Summon();
        }
    }

    public static GameController GetInstance()
    {
        return _instance;
    }


}
