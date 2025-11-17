using UnityEngine;

public class UseSpeedIncrease : Usable
{
    [SerializeField] private float _speedMultiplier = 1.1f;

    public override void Use()
    {
        if (Holder.TryGetComponent<CharacterMover>(out CharacterMover mover))
            mover.IncreaseMoveSpeedBy(_speedMultiplier);

        base.Use();
    }

}
