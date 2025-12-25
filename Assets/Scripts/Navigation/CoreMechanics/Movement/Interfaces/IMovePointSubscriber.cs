using UnityEngine;

namespace Navigation.CoreMechanics.Movement.Interfaces
{
    public interface IMovePointSubscriber
    {
        void OnNewMovePoint(Vector3 position);
    }
}