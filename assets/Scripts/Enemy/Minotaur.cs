using System;
using UnityEngine;
using System.Collections;

public class Minotaur : Enemy {

	// Use this for initialization
    public Minotaur()
    {
        Cost = Controller.MinotaurCost;
        Speed = Controller.MinotaurSpeed;
        Health = Controller.MinotaurHealth;
        
    }

    protected override void GetEffect(string effect)
    {
        switch (effect)
        {
            case "freeze":
                Freeze();
                break;
            case "splash":
                Burning();
                break;
        }
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
        iTween.Stop(EnemyGO);
        var timer = Timer.AddTimer(1f);
        timer.Done += () => { Destroy(EnemyGO); };
    }

    protected override void GetGO()
    {
        var spawn = GameController.GetInstance().Spawn;
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.Minotaur);
        EnemyGO = Instantiate(prefab, spawn.transform.position, prefab.transform.rotation) as GameObject;
    }

    protected override void GetDamage()
    {
        Health -= 10;
    }



    protected override void Cast(Action action)
    {
        action.Invoke();
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
    protected void Burning()
    {
        var burningEffectprefab = Skin.GetPrefab(Skin.GamePrefabs.FireEffect);
        var effect = Instantiate(burningEffectprefab, EnemyGO.transform.position, burningEffectprefab.transform.rotation) as GameObject;
        effect.transform.SetParent(EnemyGO.transform);
        var timer = Timer.AddTimer(3);
        timer.Done += () =>
        {
            Destroy(effect);
        };
    }
}
