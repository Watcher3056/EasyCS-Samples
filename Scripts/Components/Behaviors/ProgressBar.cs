using UnityEngine;
using UnityEngine.UI;

namespace EasyCS.Samples
{
    public class ProgressBar : EasyCSBehavior
    {
        [SerializeField] 
        private Image _fillImage;
        [SerializeField] 
        private float _maxWidth = 200f;

        public void SetProgress(float value)
        {
            value = Mathf.Clamp01(value);
            RectTransform rt = _fillImage.rectTransform;
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth * value);
        }
    }
}