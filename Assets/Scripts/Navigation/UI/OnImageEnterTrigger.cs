using UnityEngine;
using UnityEngine.EventSystems;

namespace Navigation.UI
{
    public class OnImageEnterTrigger : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Behaviour _objectToHide;
        [SerializeField] private Behaviour _objectToShow;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _objectToHide.gameObject.SetActive(false);
            _objectToShow.gameObject.SetActive(true);
        }
    }
}