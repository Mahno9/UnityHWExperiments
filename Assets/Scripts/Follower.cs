using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] Transform followItem;
    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {
        gameObject.transform.position = followItem.position + offset;
    }
}
