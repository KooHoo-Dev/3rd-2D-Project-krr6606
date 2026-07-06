using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TowerSO", menuName = "ScriptableObjects/TowerSO", order = 1)]
public class TowerSO : ScriptableObject
{
    public string towerName;
    public bool isCatTower;
    public int Level = 1;
    public int MaxHealth;

}
