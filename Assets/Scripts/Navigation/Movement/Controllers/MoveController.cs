using Navigation.Movement.Interfaces;

namespace Navigation.Movement.Controllers
{
    public abstract class MoveController : ControllerBase, IMovePointBroadcaster
    {
        public abstract float MoveSpeed { get; }
        public abstract void  SubscribeOnMovePoints(IMovePointSubscriber subscriber);
    }
}