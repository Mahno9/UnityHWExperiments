namespace Reactive.Enemies.Enemy
{
    public class Ork : Delegates.Enemies.Enemy.Enemy
    {
        private float _strength;

        public void Initialize(float strength)
        {
            _strength = strength;
        }

        public override string ToString() => $"OrkSettings with strength={_strength}";
    }
}