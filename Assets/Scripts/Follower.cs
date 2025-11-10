using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _followItem;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        gameObject.transform.position = _followItem.position + _offset;
    }
}
