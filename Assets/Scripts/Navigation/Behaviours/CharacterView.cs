using System;

using Navigation.Controllers;

using UnityEngine;
using UnityEngine.Assertions;

namespace Navigation.Behaviours
{
    public class CharacterView : MonoBehaviour
    {
        private const float Epsilon = 0.05f;

        private readonly int _isRunningKey = Animator.StringToHash("IsRunning");
        private readonly int _damagedKey   = Animator.StringToHash("Damaged");

        [SerializeField] private Animator _animator;

        private PointClickController _controller;

        private void Update()
        {
            Assert.IsNotNull(_animator);

            UpdateIsRunning();
            UpdateDamaged();
            // TODO: is dead
        }

        private void UpdateIsRunning()
        {
            bool isRunning = _controller.MoveSpeed > Epsilon;
            _animator.SetBool(_isRunningKey, isRunning);
        }

        private void UpdateDamaged()
        {
            // if (_controller.TookDamageExpirable)
            //     _animator.SetTrigger(_damagedKey);
        }
    }
}