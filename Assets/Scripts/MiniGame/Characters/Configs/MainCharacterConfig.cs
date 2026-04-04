using System;

using MiniGame.Characters.Behaviours;

using UnityEngine;

namespace MiniGame.Configs
{
    [CreateAssetMenu(fileName = "MainCharacterConfig", menuName = "Configs/Gameplay/MainCharacterConfig", order = 0)]
    public class MainCharacterConfig : ScriptableObject
    {
        [field: SerializeField]         public MainCharacterBeh Prefab        { get; private set; }
        [field: SerializeField, Min(0)] public float            MoveSpeed     { get; private set; }
        [field: SerializeField, Min(0)] public float            RotationSpeed { get; private set; }
        [field: SerializeField, Min(0)] public float            StartHealth   { get; private set; }
    }
}