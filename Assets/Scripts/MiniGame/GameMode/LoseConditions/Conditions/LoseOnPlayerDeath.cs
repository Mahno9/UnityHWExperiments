using System;

using MiniGame.Characters;

using Navigation.Utils;

using UnityEngine.Serialization;

using Random = UnityEngine.Random;

namespace MiniGame.LoseConditions
{
    [Serializable]
    public class LoseOnPlayerDeath : LoseConditionBase
    {
        private MainCharacter _mainCharacter;

        public LoseOnPlayerDeath(MainCharacter mainCharacter)
        {
            _mainCharacter = mainCharacter;
        }

        public override void Update(float deltaTime)
        {
            if (_mainCharacter.IsDead.Value)
                TriggerLost();
        }
    }
}