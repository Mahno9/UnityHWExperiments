using UnityEngine;
using UnityEngine.SceneManagement;

namespace InteractionExample.Common
{
    public class SceneReseter : MonoBehaviour
    {
        [SerializeField] private KeyCode _resetKeyCode = KeyCode.R;
        private void Update()
        {
            if (Input.GetKeyDown(_resetKeyCode))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
