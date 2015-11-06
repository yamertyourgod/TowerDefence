using System;
using UnityEngine;
using System.Collections;

public class Minotaur : Enemy {

	// Use this for initialization
    public Minotaur()
    {
        Speed = 45f;
        Health = 85;
    }

    protected override void SelfUpdate()
    {

    }

    public override void Move()
    {
        var path = GameController.GetInstance().EnemyPath;
        iTween.MoveTo(EnemyGO, iTween.Hash("path", path.ToArray(), "movetopath", true, "speed", Speed, "easetype", iTween.EaseType.linear));
        Debug.Log("MoveZombie");
    }

    public override void Die()
    {
        
    }

    protected override void GetGO()
    {
        var spawn = GameController.GetInstance().Spawn;
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.Minotaur);
        EnemyGO = Instantiate(prefab, spawn.transform.position, prefab.transform.rotation) as GameObject;
    }

    protected override void GetDamage()
    {
        
    }



    protected override void Cast(Action action)
    {
        
    }
}
