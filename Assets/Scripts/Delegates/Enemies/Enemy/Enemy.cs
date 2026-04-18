using System;
using System.Collections.Generic;

using Delegates.Enemies.Controllers;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using UnityEngine;

namespace Delegates.Enemies.Enemy
{
    public class Enemy : MonoBehaviour, IMovable, IDamageable, IDying
    {
        public float   MoveSpeed     => _mover.MoveSpeed;
        public Vector3 MoveDirection => _mover.MoveDirection;
        public Vector3 Position      => _mover.Position;

        public event Action<Enemy> OnDie;

        public bool IsDead() => IsAlive() == false;


        private AlongMoverDirectionRotator _rotator;
        private NavMeshAgentMover          _mover;
        private BrownianMovementController _moveController;

        private bool _isAlive = true;

        private Func<bool>       IsAlive;
        private List<IUpdatable> _updatableModules;


        public void Initialize(NavMeshAgentMover mover, AlongMoverDirectionRotator rotator, BrownianMovementController moveController)
        {
            _mover = mover;
            _rotator = rotator;
            _moveController = moveController;

            IsAlive = () => _isAlive;
        }

        public void SetIsAliveDelegate(Func<bool> isAlive)
        {
            IsAlive = isAlive;
        }

        public void Update()
        {
            Update(Time.deltaTime); // Update self from MonoBehaviour
        }

        public void Update(float deltaTime)
        {
            _moveController.Update(deltaTime);

            _rotator.Update(deltaTime);

            TryToDie();
        }

        private void TryToDie()
        {
            if (IsAlive() == false)
                OnDie?.Invoke(this);
        }

        public void SetMovePoint(Vector3 point) => _mover.SetMovePoint(point);

        public void TakeDamage(float damage) => _isAlive = false;

        private bool _isHovered;

        private void OnMouseEnter() => _isHovered = true;
        private void OnMouseExit() => _isHovered = false;

        private void OnGUI()
        {
            if (!_isHovered) return;

            GUIStyle style = new GUIStyle(GUI.skin.label) { fontSize = 20 };
            Vector2 mousePos = Input.mousePosition;
            Rect labelRect = new Rect(mousePos.x + 10, Screen.height - mousePos.y - 30, 500, 30);
            
            GUI.Box(labelRect, "");
            GUI.Label(labelRect, ToString(), style);
        }
    }
}