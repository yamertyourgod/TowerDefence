using System;
using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    public int Health;
    public float Speed;
    public int Mana;
    public GameObject EnemyGO;
    public bool CanCast = true;
    protected float CoolDown;
    protected GameObject _healthbar;
    private CollisionDetector _coll;

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
        _healthbar = Instantiate(Skin.GetPrefab(Skin.GamePrefabs.Healthbar));
        _healthbar.transform.position = camera.WorldToScreenPoint(EnemyGO.transform.position);
        _healthbar.transform.SetParent(FindObjectOfType<GUI>().transform);
            
    }

    protected void Update()
    {
        if (Health < 1)
        {
            Debug.Log("Die");
            Die();
        }
        SelfUpdate();
        if (_healthbar != null)
        {
            var camera = FindObjectOfType<Camera>();
            _healthbar.transform.position = camera.WorldToScreenPoint(EnemyGO.transform.position);
        }
    }

}
