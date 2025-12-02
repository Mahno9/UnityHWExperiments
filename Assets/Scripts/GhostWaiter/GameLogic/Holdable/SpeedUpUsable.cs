using UnityEngine;

public class SpeedUpUsable : Usable
{
    [SerializeField] private float _speedMultiplier = 1.1f;

    public override void Use(GameObject targetObject)
    {
        if (targetObject.TryGetComponent<CharacterMover>(out CharacterMover mover))
            mover.IncreaseMoveSpeedBy(_speedMultiplier);

        base.Use(targetObject);
    }
}
