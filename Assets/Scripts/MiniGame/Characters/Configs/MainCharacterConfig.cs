using Cinemachine;

using MiniGame.Characters;
using MiniGame.CoreMechanics.Shooting;

using UnityEngine;

namespace MiniGame.Configs
{
    [CreateAssetMenu(fileName = "MainCharacterConfig", menuName = "Configs/Gameplay/MainCharacterConfig", order = 0)]
    public class MainCharacterConfig : ScriptableObject
    {
        [field: SerializeField]         public MainCharacter            Prefab           { get; private set; }
        [field: SerializeField]         public CinemachineVirtualCamera VirtualCamera    { get; private set; }
        [field: SerializeField]         public Projectile               ProjectilePrefab { get; private set; }
        [field: SerializeField, Min(0)] public float                    MoveSpeed        { get; private set; }
        [field: SerializeField, Min(0)] public float                    RotationSpeed    { get; private set; }
        [field: SerializeField, Min(0)] public float                    StartHealth      { get; private set; }
    }
}