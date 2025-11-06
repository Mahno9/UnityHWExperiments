using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Object _effectOnCollect;

    public void OnCollect()
    {
        if (_effectOnCollect != null)
            Instantiate(_effectOnCollect, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
    }
}
