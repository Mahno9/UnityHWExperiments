namespace Reactive.Enemies.Enemy
{
    public class Elf : Delegates.Enemies.Enemy.Enemy
    {
        private float _dexterity;

        public void Initialize(float dexterity)
        {
            _dexterity = dexterity;
        }

        public override string ToString() => $"Elf with dexterity={_dexterity}";
    }
}