using UnityEngine;
using UnityEngine.EventSystems;

namespace Navigation.UI
{
    public class OnImageLeaveTrigger : MonoBehaviour, IPointerExitHandler
    {
        [SerializeField] private Behaviour _objectToHide;
        [SerializeField] private Behaviour _objectToShow;

        public void OnPointerExit(PointerEventData eventData)
        {
            _objectToHide.gameObject.SetActive(false);
            _objectToShow.gameObject.SetActive(true);
        }
    }
}