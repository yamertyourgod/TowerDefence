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
    public static Towers CurrentTowerType;
    private static GameObject _map;
    // Use this for initialization
	void Start ()
	{
	    if (_instance != null)
	        return;
	    _instance = this;
        _map = GameObject.Find("Map");
	    ShowMap(false);
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            var tower = new SimpleTower();
            tower.Build(new Vector3(-39,3,92));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            var tower = new FrozenTower();
            tower.Build(new Vector3(-39, 3, 92));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            var tower = new FrozenTower();
            tower.Build(new Vector3(-39, 3, 92));
        }
    }

    public static void StartGame()
    {
        
    }

    public static GameController GetInstance()
    {
        return _instance;
    }

    public static void BuildTower(Vector3 buildPos)
    {
        switch (CurrentTowerType)
        {
            case Towers.Simple:
                var simple = new SimpleTower();
                simple.Build(buildPos);
                break;
            case Towers.Splash:
                var splash = new SplashTower();
                splash.Build(buildPos);
                break;
            case Towers.Frozen:
                var frozen = new FrozenTower();
                frozen.Build(buildPos);
                break;
        }
    }

    public static void ShowMap(bool show)
    {
        _map.SetActive(show);
    }

    public enum Towers
    {
        Simple,
        Splash,
        Frozen
    }

}
