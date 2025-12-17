using UnityEngine;

namespace Navigation.Controllers
{
    public interface IMovePointSubscriber
    {
        void OnNewMovePoint(Vector3 position);
    }
}