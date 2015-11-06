using UnityEngine;
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
        }
        return go;
    }
    public enum GamePrefabs
    {
        Cell,
        Zombie,
        Minotaur,
        Mage
    }
}
