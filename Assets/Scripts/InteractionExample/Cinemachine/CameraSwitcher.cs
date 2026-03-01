using Cinemachine;

using UnityEngine;
using UnityEngine.Assertions;

namespace InteractionExample.Cinemachine
{
    [System.Serializable]
    public struct KeyCameraPair
    {
        public KeyCode                  Key;
        public CinemachineVirtualCamera Camera;
    }

    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private KeyCameraPair[]  _cameras;

        private void Awake()
        {
            Assert.IsTrue(_cameras.Length > 0);

            SetActiveCamera(_cameras[0].Key);
        }

        private void Update()
        {
            foreach (KeyCameraPair keyCameraPair in _cameras)
            {
                if (Input.GetKeyDown(keyCameraPair.Key))
                    SetActiveCamera(keyCameraPair.Key);
            }
        }

        private void SetActiveCamera(KeyCode activeCameraKey)
        {
            foreach (KeyCameraPair keyCameraPair in _cameras)
            {
                bool isActive = keyCameraPair.Key == activeCameraKey;
                keyCameraPair.Camera.gameObject.SetActive(isActive);
            }
        }
    }
}