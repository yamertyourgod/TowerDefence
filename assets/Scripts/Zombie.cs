using UnityEngine;
using System.Collections;

public class Zombie : Enemy {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    override public void Move()
    {
        Debug.Log("MoveZombie");
    }

    override public void Die()
    {
        Debug.Log("DieZombie");
    }

    override protected void GetGO()
    {
        EnemyGO = Skin.GetPrefab(Skin.GamePrefabs.Zombie);
    }

    protected override void GetDamage()
    {
        Debug.Log("GetDamage");
    }

    protected override void Collide(string t)
    {
        Debug.Log("Collided with "+t);
    }
}
