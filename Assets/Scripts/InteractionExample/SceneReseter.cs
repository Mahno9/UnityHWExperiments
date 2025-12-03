using System;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReseter : MonoBehaviour
{
    [SerializeField] private KeyCode _resetKeyCode = KeyCode.R;
    private void Update()
    {
        if (Input.GetKeyDown(_resetKeyCode))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
