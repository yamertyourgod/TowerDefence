using System;
using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour
{
    public delegate void HitDelegate(Collider col);
    public delegate void UpdateDelegate();
    public event HitDelegate Hit;
    public event UpdateDelegate UpdateEvent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (UpdateEvent != null) UpdateEvent();
	}

    void OnTriggerEnter(Collider obj)
    {
        if (Hit != null) Hit(obj);
    }
}
