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
        CoolDown = 5f;
    }

    protected override void GetEffect(string effect)
    {
        switch (effect)
        {
            case "freeze":
                Freeze();
                break;
        }
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
        iTween.Stop(EnemyGO);
        var timer = Timer.AddTimer(1f);
        timer.Done += () => { Destroy(EnemyGO); };

    }

    protected override void GetGO()
    {
        var spawn = GameController.GetInstance().Spawn;
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.Mage);
        EnemyGO = Instantiate(prefab, spawn.transform.position, prefab.transform.rotation) as GameObject;
    }

    protected override void GetDamage()
    {
        Health -= 10;
        Debug.Log("Health = "+Health);
    }

    protected override void Cast(Action action)
    {
        if (CanCast)
        {
            action.Invoke();
            var timer = Timer.AddTimer(CoolDown);
            timer.Done += () => { CanCast = true; };
        }

    }

    protected void Heal()
    {
        if (Mana > 15)
        {
            Mana -= 15;
            Health += 40;
            Debug.Log("Health " + Health + ";" + " Mana " + Mana);
        }
    }

    protected void Freeze()
    {
        var tween = EnemyGO.GetComponent<iTween>();
        tween.isRunning = false;
        var timer = Timer.AddTimer(3);
        timer.Done += () =>
        {
            tween.isRunning = true;
        };
    }


}
