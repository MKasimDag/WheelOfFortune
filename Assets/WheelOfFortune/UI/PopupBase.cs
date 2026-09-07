using UnityEngine;

namespace WheelOfFortune.UI
{
    public abstract class PopupBase : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnValidate()
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected virtual void Awake()
        {
            Hide();
        }

        public virtual void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}