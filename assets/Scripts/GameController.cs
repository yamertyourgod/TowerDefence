using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public int Gold;
    public int EnemiesInWave;
    public int KilledEnemies;
    public int ZombieHealth;
    public int MinotaurHealth;
    public int MageHealth;
    public int MageMana;
    public int MageCastCooldown;
    public float ZombieSpeed;
    public float MinotaurSpeed;
    public float MageSpeed;
    public int ZombieCost;
    public int MinotaurCost;
    public int MageCost;
    public int SimpleTowerCost;
    public int SplashTowerCost;
    public int FrozenTowerCost;
    public int SimpleTowerRadius;
    public int SplashTowerRadius;
    public int FrozenTowerRadius;
    public float SimpleTowerReloadTime;
    public float SplashTowerReloadTime;
    public float FrozenTowerReloadTime;
    public float SimpleTowerCanonSpeed;
    public float SplashTowerCanonSpeed;
    public float FrozenTowerCanonSpeed;
    public float SplashRadius;

    public List<Vector3> EnemyPath;
    public GameObject Spawn;
    public GameObject End;
    public List<GridCell> ChangedCells; 
    private static GameController _instance;
    public static Towers CurrentTowerType;
    private static GameObject _map;
    private List<Action> _wave = new List<Action>();
    private List<GameObject> _enemies = new List<GameObject>();
    private static List<GameObject> _towers = new List<GameObject>();
    private int _originGold;
    private int _originEnemiesInWave;
    private GUI _gui;
    private CollisionDetector _endColl;
    // Use this for initialization
    void Start ()
	{
	    if (_instance != null)
	        return;
	    _instance = this;
        _map = GameObject.Find("Map");
	    ShowMap(false);
        _gui = FindObjectOfType<GUI>();
        _gui.StartGui();
        _originGold = Gold;
        _endColl = End.AddComponent<CollisionDetector>();
        ChangedCells = new List<GridCell>();
        _endColl.Hit += EndReached;
        _originEnemiesInWave = EnemiesInWave;

	}

    private void EndReached(Collider col)
    {
        if (col.tag == "enemy")
        {
            GameOver();
        }
    }

    // Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameOver();
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
            tower.Build(new Vector3(-39, 3, 92));
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

    public  void CreateWave()
    {
        _wave.Clear();
        for (var i = 0; i < EnemiesInWave; i++)
        {
            var enemyCase = Random.Range(0, 3);
            switch (enemyCase)
            {
                case 0:
                    _wave.Add(() =>
                    {
                        var zombie = new Zombie();
                        zombie.Summon();
                        _enemies.Add(zombie.EnemyGO);
                    });
                    break;
                case 1:
                    _wave.Add(() =>
                    {
                        var minotaur = new Minotaur();
                        minotaur.Summon();
                        _enemies.Add(minotaur.EnemyGO);
                    });
                    break;
                case 2:
                    _wave.Add(() =>
                    {
                        var mage = new Mage();
                        mage.Summon();
                        _enemies.Add(mage.EnemyGO);
                    });
                    break;
            }
        }
        EnemiesInWave+=5;
    }

    public void StartWave()
    {
        StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        foreach (var enemyAction in _wave)
        {
            var time = Random.Range(0.5f, 2f);
            enemyAction.Invoke();
            yield return new WaitForSeconds(time);
        }
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
                _towers.Add(simple.TowerGO);
                break;
            case Towers.Splash:
                var splash = new SplashTower();
                splash.Build(buildPos);
                _towers.Add(splash.TowerGO);
                break;
            case Towers.Frozen:
                var frozen = new FrozenTower();
                frozen.Build(buildPos);
                _towers.Add(frozen.TowerGO);
                break;
        }
    }

    public static void ShowMap(bool show)
    {
        _map.SetActive(show);
    }

    public void GameOver()
    {
        StopAllCoroutines();
        _wave.Clear();

        foreach (var enemy in _enemies)
        {
            Destroy(enemy);
        }
        _enemies.Clear();
        foreach (var tower in _towers)
        {
            Destroy(tower);
        }
        _towers.Clear();
        foreach (var changedCell in ChangedCells)
        {
            changedCell.State = changedCell.OriginalState;
        }
        ChangedCells.Clear();
        _gui.Reset();
        _gui.SetScreen(GUI.GameScreens.GameOverScreen);
        var timer = Timer.AddTimer(3);
        timer.Done += () => _gui.SetScreen(GUI.GameScreens.StartScreen);
        Gold = _originGold;
        EnemiesInWave = _originEnemiesInWave;
        KilledEnemies = 0;
    }

    public enum Towers
    {
        Simple,
        Splash,
        Frozen
    }

}
