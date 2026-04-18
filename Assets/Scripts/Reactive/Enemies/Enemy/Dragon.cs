namespace Reactive.Enemies.Enemy
{
    public class Dragon : Delegates.Enemies.Enemy.Enemy
    {
        private float _wisdom;

        public void Initialize(float wisdom)
        {
            _wisdom = wisdom;
        }

        public override string ToString() => $"DragonSettings with wisdom={_wisdom}";
    }
}