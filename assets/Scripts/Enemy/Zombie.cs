using System;
using UnityEngine;
using System.Collections;

public class Zombie : Enemy {

	// Use this for initialization
    public Zombie()
    {
        Cost = Controller.ZombieCost;
        Speed = Controller.ZombieSpeed;
        Health = Controller.ZombieHealth;
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

    }
    override public void Move()
    {
        var path = GameController.GetInstance().EnemyPath;
        iTween.MoveTo(EnemyGO,iTween.Hash("path", path.ToArray(), "movetopath",  true, "speed", Speed, "easetype", iTween.EaseType.linear));
    }

    override public void Die()
    {
        iTween.Stop(EnemyGO);
        var timer = Timer.AddTimer(1f);
        timer.Done += () => { Destroy(EnemyGO); };
    }

    override protected void GetGO()
    {
        var spawn = GameController.GetInstance().Spawn;
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.Zombie);
        EnemyGO = Instantiate(prefab, spawn.transform.position, prefab.transform.rotation)as GameObject;
    }

    protected override void GetDamage()
    {
        Health -= 10;
    }

    protected void Freeze()
    {
        var tween = EnemyGO.GetComponent<iTween>();
        tween.isRunning = false;
        var freezeEffectprefab = Skin.GetPrefab(Skin.GamePrefabs.FreezEffect);
        var effect = Instantiate(freezeEffectprefab, EnemyGO.transform.position, freezeEffectprefab.transform.rotation) as GameObject;
        effect.transform.SetParent(EnemyGO.transform);
        var timer = Timer.AddTimer(3);
        timer.Done += () =>
        {
            tween.isRunning = true;
            Destroy(effect);
        };
    }

    protected override void Cast(Action action)
    {
       action.Invoke();
    }
}
