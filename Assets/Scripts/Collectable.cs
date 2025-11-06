using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Object effectOnCollect;

    public void OnCollect()
    {
        if (effectOnCollect != null)
            Instantiate(effectOnCollect, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
    }
}
