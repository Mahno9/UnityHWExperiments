using UnityEngine;

public abstract class GravityUpdaterStrategy : MonoBehaviour
{
    protected float _gravityValue;

    public virtual void OnGrounding(Vector3 normal) { }

    public virtual void OnUpdate(Vector3 mainObjectPos) { }

    private void Awake()
    {
        _gravityValue = Physics.gravity.magnitude;
    }
}

