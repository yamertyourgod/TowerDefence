using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public int Health;
    public float Speed;
    public GameObject EnemyGO;
    public bool CanCast = true;
    public int Cost;
    public bool Dead;
    protected float CoolDown;
    protected GameObject Healthbar;
    protected int OriginHealth;
    private CollisionDetector _coll;
    private Image _healthbarFront;
    private Image _healtbarBack;
    protected GameController Controller = GameController.GetInstance();
    // Use this for initialization
    public abstract void Move();
    public abstract void Die();
    protected abstract void GetGO();
    protected abstract void GetDamage();
    protected abstract void GetEffect(string tag);
    protected abstract void SelfUpdate();
    protected abstract void Cast(Action action);


    public void Summon()
    {
        GetGO();
        _coll = EnemyGO.AddComponent<CollisionDetector>(); 
        _coll.Hit += Collide;
        _coll.UpdateEvent += Update;
        EnemyGO.GetComponent<Collider>().isTrigger = true;      
        EnemyGO.tag = "enemy";
        OriginHealth = Health;
        AddHealthbar();
        //AddTriger();
        Move();
    }


    protected  void Collide(Collider col)
    {
        Debug.Log("ColliderTag "+ col.tag);
        if (col.tag == "bullet")
        {
            GetDamage();
            Debug.Log("Health = "+Health);
            Destroy(col.gameObject);
        }
        if (col.tag == "freeze")
        {
            GetEffect(col.tag);
        }
        if (col.tag == "splash")
        {
            GetDamage();
        }
    }
    private void AddHealthbar()
    {
        var camera = FindObjectOfType<Camera>();
        Healthbar = Instantiate(Skin.GetPrefab(Skin.GamePrefabs.Healthbar));
        Healthbar.transform.position = camera.WorldToScreenPoint(EnemyGO.transform.position);
        Healthbar.transform.SetParent(FindObjectOfType<GUI>().transform);
        _healthbarFront = Healthbar.transform.GetChild(0).GetComponent<Image>();
        _healtbarBack = Healthbar.GetComponent<Image>();

    }

    protected void Update()
    {
        if (!Dead&&Health < 1)
        {
            Debug.Log("Die");
            Die();
            Dead = true;
            Destroy(Healthbar);
            AddDropCost();
            Controller.Gold += Cost;
            Controller.KilledEnemies++;
        }
        
        SelfUpdate();
        if (Healthbar != null)
        {
            var camera = FindObjectOfType<Camera>();
            Healthbar.transform.position = camera.WorldToScreenPoint(EnemyGO.transform.position)+Vector3.up*20;
            _healthbarFront.rectTransform.sizeDelta = new Vector2(_healtbarBack.rectTransform.sizeDelta.x / OriginHealth * Health, _healtbarBack.rectTransform.sizeDelta.y);
            if (_healthbarFront.rectTransform.sizeDelta.x < _healtbarBack.rectTransform.sizeDelta.x/OriginHealth)
            {
                _healthbarFront.rectTransform.sizeDelta = new Vector2(0, _healtbarBack.rectTransform.sizeDelta.y);
            }    
        }
    }

    private void AddDropCost()
    {
        var camera = FindObjectOfType<Camera>();
        var dropText = Instantiate(Skin.GetPrefab(Skin.GamePrefabs.EnemyDropCost));
        dropText.GetComponent<Text>().text = "+" + Cost;
        dropText.transform.position = camera.WorldToScreenPoint(EnemyGO.transform.position);
        dropText.transform.SetParent(FindObjectOfType<GUI>().transform);
        var timer = Timer.AddTimer(1f);
        timer.Done += () => { Destroy(dropText); };
    }
}
