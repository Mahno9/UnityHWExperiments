using UnityEngine;

public class Replaceable : Holdable
{
    [SerializeField] private string _requiredTag;

    public string RequiredTag => _requiredTag;
}
