using System;
using HamitEfe.SCC;
using UnityEngine;

public abstract class CharacterComponent : MonoBehaviour
{
    public Character Character { get; private set; }
    protected void Awake()
    {
        Character = GetComponent<Character>();
    }
}
