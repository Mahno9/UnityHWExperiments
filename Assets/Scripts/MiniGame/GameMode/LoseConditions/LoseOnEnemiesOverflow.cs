using Navigation.Utils;

using UnityEngine;

namespace MiniGame.LoseConditions
{
    public class LoseOnEnemiesOverflow : LoseConditionBase
    {
        public override void Update(float deltaTime)
        {
            // TODO
            if (Random.Range(0, 100) == 1)
                IsLostVar.Value = true;
        }
    }
}