using UnityEngine;
using System.Collections;

public class SkinResources : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject ZombiePrefab;
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
