using System;
using UnityEngine;
using System.Collections;

public class Zombie : Enemy {

	// Use this for initialization
    public Zombie()
    {
        Speed = 20f;
        Health = 20;
    }

    protected override void GetEffect()
    {
        
    }

    protected override void SelfUpdate()
    {

    }
    override public void Move()
    {
        var path = GameController.GetInstance().EnemyPath;
        iTween.MoveTo(EnemyGO,iTween.Hash("path", path.ToArray(), "movetopath",  true, "speed", Speed, "easetype", iTween.EaseType.linear));
    }

    override public void Die()
    {
        
    }

    override protected void GetGO()
    {
        var spawn = GameController.GetInstance().Spawn;
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.Zombie);
        EnemyGO = Instantiate(prefab, spawn.transform.position, prefab.transform.rotation)as GameObject;
    }

    protected override void GetDamage()
    {
        Debug.Log("GetDamage");
    }



    protected override void Cast(Action action)
    {
        throw new NotImplementedException();
    }
}
