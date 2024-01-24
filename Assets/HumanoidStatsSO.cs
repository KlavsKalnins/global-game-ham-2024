using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanoidStatsSO", menuName = "HAM/HumanoidStatsSO", order = 1)]
public class HumanoidStatsSO : ScriptableObject
{
    public string Name;
    public int Health;
    public int Damage;
    public int Dodge;
    public float WalkingSpeed;
}
