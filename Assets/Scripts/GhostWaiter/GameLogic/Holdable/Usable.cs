using UnityEngine;

public abstract class Usable : Holdable
{
    [SerializeField] private GameObject _useFx;

    public virtual void Use(GameObject targetObject)
    {
        if (_useFx is not null)
            Instantiate(_useFx, transform.position, _useFx.transform.rotation);

        Destroy(gameObject);
    }
}
