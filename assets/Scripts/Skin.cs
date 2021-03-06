﻿using UnityEngine;
using System.Collections;

public static class Skin
{
    private static readonly SkinResources SkinResources = SkinResources.GetInstance();
    public static GameObject GetPrefab(GamePrefabs prefab)
    {
        GameObject go = null;
        switch (prefab)
        {
            case GamePrefabs.Cell:
                go = SkinResources.CellPrefab;
                break;
            case GamePrefabs.Zombie:
                go = SkinResources.ZombiePrefab;
                break;
            case GamePrefabs.Minotaur:
                go = SkinResources.MinotaurPrefab;
                break;
            case GamePrefabs.Mage:
                go = SkinResources.MagePrefab;
                break;
            case GamePrefabs.SimpleTower:
                go = SkinResources.SimpleTowerPrefab;
                break;
            case GamePrefabs.SplashTower:
                go = SkinResources.SplashTowerPrefab;
                break;
            case GamePrefabs.FrozenTower:
                go = SkinResources.FrozenTowerPrefab;
                break;
            case GamePrefabs.Bullet:
                go = SkinResources.BulletPrefab;
                break;
            case GamePrefabs.Freeze:
                go = SkinResources.FreezePrefab;
                break;
            case GamePrefabs.Splash:
                go = SkinResources.SplashPrefab;
                break;
            case GamePrefabs.Healthbar:
                go = SkinResources.HealthbarPrefab;
                break;
            case GamePrefabs.Manabar:
                go = SkinResources.ManabarPrefab;
                break;
            case GamePrefabs.HealingEffect:
                go = SkinResources.HealingEffectPrefab;
                break;
            case GamePrefabs.FreezEffect:
                go = SkinResources.FreezEffectPrefab;
                break;
            case GamePrefabs.EnemyDropCost:
                go = SkinResources.EnemyDropCostPrefab;
                break;
            case GamePrefabs.FireEffect:
                go = SkinResources.FireEffectPrefab;
                break;
        }
        return go;
    }
    public enum GamePrefabs
    {
        Cell,
        Zombie,
        Minotaur,
        Mage,
        SimpleTower,
        SplashTower,
        FrozenTower,
        Bullet,
        Freeze,
        Splash,
        Healthbar,
        Manabar,
        HealingEffect,
        FreezEffect,
        FireEffect,
        EnemyDropCost

    }
}
