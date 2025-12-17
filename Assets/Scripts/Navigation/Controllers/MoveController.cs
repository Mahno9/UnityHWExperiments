using UnityEngine;

namespace Navigation.Controllers
{
    public abstract class MoveController : ControllerBase
    {
        public abstract     float   MoveSpeed        { get; }
    }
}