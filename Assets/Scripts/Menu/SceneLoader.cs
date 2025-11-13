using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _sceneNameToLoad;

    private void Awake()
    {
        var button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (Application.CanStreamedLevelBeLoaded(_sceneNameToLoad))
            SceneManager.LoadScene(_sceneNameToLoad);
    }
}
