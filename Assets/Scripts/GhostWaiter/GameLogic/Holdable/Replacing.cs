using UnityEngine;

public class Replacing : UseThrow
{
    [SerializeField] private string _tag;

    public string Tag => _tag;
}
