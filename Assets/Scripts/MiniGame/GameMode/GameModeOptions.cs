using System;

using Common.Utils;

using MiniGame.LoseConditions;
using MiniGame.WinConditions;

using UnityEngine;

namespace MiniGame
{
    [Serializable]
    public class GameModeOptions
    {
        [SerializeReference] [SubclassSelector]
        public IWinCondition  WinCondition;
        [SerializeReference] [SubclassSelector]
        public ILoseCondition LoseCondition;
    }
}