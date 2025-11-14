using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _sceneNameToLoad;

    private void Awake()
    {
        if (TryGetComponent<Button>(out Button button))
        {
            if (button != null)
                button.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError("SceneLoader requires a Button component.");
        }
    }

    private void OnButtonClicked()
    {
        if (Application.CanStreamedLevelBeLoaded(_sceneNameToLoad))
            SceneManager.LoadScene(_sceneNameToLoad);
    }
}
