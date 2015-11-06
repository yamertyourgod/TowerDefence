using System;
using UnityEngine;
using System.Collections;

public class Mage : Enemy
{
    // Use this for initialization
    public Mage()
    {
        Health = 85;
        Mana = 50;
        Speed = 20f;
    }

    protected override void GetEffect()
    {
        
    }

    protected override void SelfUpdate()
    {
        if (Health < 70)
        {
            Cast(Heal);
        }
    }
    public override void Move()
    {
        var path = GameController.GetInstance().EnemyPath;
        iTween.MoveTo(EnemyGO, iTween.Hash("path", path.ToArray(), "movetopath", true, "speed", Speed, "easetype", iTween.EaseType.linear));
    }

    public override void Die()
    {

    }

    protected override void GetGO()
    {
        var spawn = GameController.GetInstance().Spawn;
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.Mage);
        EnemyGO = Instantiate(prefab, spawn.transform.position, prefab.transform.rotation) as GameObject;
    }

    protected override void GetDamage()
    {
        Health -= 5;
        Debug.Log("Health = "+Health);
    }

    protected override void Cast(Action action)
    {
        action.Invoke();
    }

    protected void Heal()
    {
        Mana -= 15;
        Health += 40;
        Debug.Log("Health "+Health+";"+" Mana "+Mana);
    }

    
}
