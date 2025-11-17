using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField] private Transform _joint;

    private Usable _currentUsable;

    public Usable ExtractUsable()
    {
        Usable _extractedUsable = _currentUsable;
        _currentUsable = null;
        return _extractedUsable;
    }

    public bool InlayUsable(Usable usable)
    {
        if (IsEmpty)
            _currentUsable = usable;
        else
            return false;

        _currentUsable.transform.SetParent(_joint);
        _currentUsable.transform.localPosition = Vector3.zero;

        return true;
    }

    public bool IsEmpty => _currentUsable == null;
}
