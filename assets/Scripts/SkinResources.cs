using UnityEngine;
using System.Collections;

public class SkinResources : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject ZombiePrefab;
    public GameObject MinotaurPrefab;
    public GameObject MagePrefab;
    public GameObject SimpleTowerPrefab;
    public GameObject SplashTowerPrefab;
    public GameObject FrozenTowerPrefab;
    public GameObject BulletPrefab;
    public GameObject FreezePrefab;
    public GameObject SplashPrefab;
    public GameObject HealthbarPrefab;
    public GameObject ManabarPrefab;
    public GameObject HealingEffectPrefab;
    public GameObject FreezEffectPrefab;
    public GameObject EnemyDropCostPrefab;



    // Use this for initialization
    private static SkinResources _instance;
    void Start ()
    {
        _instance = this;
    }

    public SkinResources()
    {
        if(_instance!=null)return;
        _instance = this;
    }
	// Update is called once per frame
	void Update () {
	
	}

    public static SkinResources GetInstance()
    {
        return _instance;
    }
}
