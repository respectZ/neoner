using UnityEngine;

using Neoner.Objects;
using UnityEngine.UI;

namespace Neoner.UI
{
    public class Neon : MonoBehaviour
    {
        [SerializeField]
        private Sprite _shapeRed;
        [SerializeField]
        private Sprite _shapeGreen;
        [SerializeField]
        private Sprite _shapeLightBlue;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Toggle(NeonColor color)
        {
            switch (color)
            {
                case NeonColor.Red:
                    _image.sprite = _shapeRed;
                    _image.color = new Color(1f, 0f, 0f);
                    // Readjust offset
                    // _image.rectTransform.position = new Vector2(0, 50f);
                    // _image.rectTransform.sizeDelta = new Vector2(100f, 100f);
                    break;
                case NeonColor.Green:
                    _image.sprite = _shapeGreen;
                    _image.color = new Color(0f, 1f, 0f);
                    // Readjust offset
                    // _image.rectTransform.position = new Vector2(0, 50f);
                    // _image.rectTransform.sizeDelta = new Vector2(100f, 100f);
                    break;
                case NeonColor.LightBlue:
                    _image.sprite = _shapeLightBlue;
                    _image.color = new Color(0f, 1f, 238 / 255f, 1f);
                    // Readjust offset
                    // _image.rectTransform.position = new Vector2(0, 50f);
                    // _image.rectTransform.sizeDelta = new Vector2(100f, 100f);
                    break;
            }
        }
    }
}