using System;
using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour
{
    public delegate void HitDelegate(string tag);
    public event HitDelegate Hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Hit " + obj.tag);
        if (Hit != null) Hit(obj.tag);
    }
}
