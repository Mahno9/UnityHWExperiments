using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GhostWaiter.UI
{
    public class ProgressbarValueUpdater : MonoBehaviour
    {
        [FormerlySerializedAs("_container")] [SerializeField] private Progressable _data;
        [SerializeField] private Image _progressbar;

        private void Update()
        {
            Assert.IsNotNull(_data);

            _progressbar.fillAmount = _data.GetProgress();
        }
    }
}