using System;

using Navigation.Utils;

using UnityEngine.Serialization;

using Random = UnityEngine.Random;

namespace MiniGame.LoseConditions
{
    [Serializable]
    public class LoseOnPlayerDeath : LoseConditionBase
    {
        public override void Update(float deltaTime)
        {
            // TODO
            if (Random.Range(0, 100) == 1)
                IsLostVar.Value = true;
        }
    }
}