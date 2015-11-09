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

    protected void Update()
    {
        if (Health < 1)
        {
            Debug.Log("Die");
            Die();
        }
        SelfUpdate();
    }

}
