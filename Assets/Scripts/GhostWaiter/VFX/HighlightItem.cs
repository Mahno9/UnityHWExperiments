using UnityEngine;

namespace GhostWaiter.VFX
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class HighlightItem : MonoBehaviour
    {
        private const string HIGHLIGHT_CHILD_NAME = "Highlight";

        [SerializeField] private Material _highlightMaterial;
        [SerializeField] private float _highlightSize = 0.2f;

        private GameObject _highlightCopy;

        private void Start()
        {
            EnableHighlight();
        }

        public void DisableHighlight()
        {
            _highlightCopy.SetActive(false);
        }

        public void EnableHighlight()
        {
            if (_highlightMaterial != null)
                CreateHighlightCopy();
            else
                _highlightCopy.SetActive(true);
        }

        private void CreateHighlightCopy()
        {
            _highlightCopy = new GameObject(HIGHLIGHT_CHILD_NAME);
            _highlightCopy.transform.SetParent(transform);
            _highlightCopy.transform.localPosition = Vector3.zero;
            _highlightCopy.transform.localRotation = Quaternion.identity;
            _highlightCopy.transform.localScale = Vector3.one * (1 + _highlightSize);

            MeshFilter originalMeshFilter = GetComponent<MeshFilter>();

            MeshFilter copyMeshFilter = _highlightCopy.AddComponent<MeshFilter>();
            copyMeshFilter.sharedMesh = originalMeshFilter.sharedMesh;

            MeshRenderer copyMeshRenderer = _highlightCopy.AddComponent<MeshRenderer>();
            copyMeshRenderer.materials = new Material[] { _highlightMaterial };
        }
    }
}
