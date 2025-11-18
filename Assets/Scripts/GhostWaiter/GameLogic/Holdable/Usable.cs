using UnityEngine;

public abstract class Usable : Holdable
{
    [SerializeField] private GameObject _useFx;

    public virtual void Use()
    {
        if (_useFx != null)
            Instantiate(_useFx, transform.position, _useFx.transform.rotation);

        Destroy(gameObject);
    }

    public virtual void Init(WaiterGameState gameState)
    { }
}
