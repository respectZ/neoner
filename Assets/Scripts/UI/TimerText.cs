using UnityEngine;
using TMPro;

namespace Neoner.UI
{
    public class TimerText : MonoBehaviour
    {
        private float _startTime = 0f;
        private float _currentTime = 0f;
        public float CurrentTime { get { return _currentTime; } }
        private TMP_Text _textMesh;
        public bool IsEnabled = true;

        private void Start()
        {
            _textMesh = GetComponent<TMP_Text>();
            _startTime = Time.time;
        }

        private void Update()
        {
            if (!IsEnabled)
                return;
            _currentTime = Time.time - _startTime;
            // Format string -> MM:SS.ms
            _textMesh.text = string.Format("{0:00}:{1:00}.{2:00}", _currentTime / 60, _currentTime % 60, (_currentTime * 100) % 100);
        }
    }
}