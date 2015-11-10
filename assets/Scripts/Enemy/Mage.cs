using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mage : Enemy
{
    public int Mana;
    protected int OriginMana;
    protected GameObject Manabar;
    private Image _manabarFront;
    private Image _manabarBack;
    // Use this for initialization
    public Mage()
    {
        Cost = Controller.MageCost;
        Health = Controller.MageHealth;
        Mana = Controller.MageMana;
        Speed = Controller.MageSpeed;
        CoolDown = Controller.MageCastCooldown;
        OriginMana = Mana;
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
        if (Health < 70)
        {
            Cast(Heal);
        }
        if (Health > OriginHealth)
        {
            Health = OriginHealth;
        }
        if (Healthbar != null)
        {
            var camera = FindObjectOfType<Camera>();
            Manabar.transform.position = camera.WorldToScreenPoint(EnemyGO.transform.position) + Vector3.up * 30;
            _manabarFront.rectTransform.sizeDelta = new Vector2(_manabarBack.rectTransform.sizeDelta.x / OriginMana * Mana, _manabarBack.rectTransform.sizeDelta.y);
            if (_manabarFront.rectTransform.sizeDelta.x < _manabarBack.rectTransform.sizeDelta.x / OriginMana)
            {
                _manabarFront.rectTransform.sizeDelta = new Vector2(0, _manabarBack.rectTransform.sizeDelta.y);
            }
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
        Destroy(Manabar);
    }

    protected override void GetGO()
    {
        var spawn = GameController.GetInstance().Spawn;
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.Mage);
        EnemyGO = Instantiate(prefab, spawn.transform.position, prefab.transform.rotation) as GameObject;
        AddManabar();
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
            CanCast = false;
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
            var healingEffectprefab = Skin.GetPrefab(Skin.GamePrefabs.HealingEffect);
            var effect = Instantiate(healingEffectprefab, EnemyGO.transform.position,healingEffectprefab.transform.rotation) as GameObject;
            effect.transform.SetParent(EnemyGO.transform);
            var timer = Timer.AddTimer(3f);
            timer.Done += () => {Destroy(effect);};
        }
    }

    private void AddManabar()
    {
        var camera = FindObjectOfType<Camera>();
        Manabar = Instantiate(Skin.GetPrefab(Skin.GamePrefabs.Manabar));
        Manabar.transform.position = camera.WorldToScreenPoint(EnemyGO.transform.position);
        Manabar.transform.SetParent(FindObjectOfType<GUI>().transform);
        _manabarFront = Manabar.transform.GetChild(0).GetComponent<Image>();
        _manabarBack = Manabar.GetComponent<Image>();

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
