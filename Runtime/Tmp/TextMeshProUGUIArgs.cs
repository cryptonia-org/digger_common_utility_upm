using TMPro;
using UnityEngine;

namespace CommonUtility.Tmp
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUGUIArgs : MonoBehaviour
    {
        [SerializeField]
        private string _text;
        [SerializeField, HideInInspector]
        private TextMeshProUGUI _textMesh;

        public TextMeshProUGUI Mesh => _textMesh;

        public void SetArgs(params object[] args) =>
            _textMesh.text = string.Format(_text, args);

        private void OnValidate()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            if (string.IsNullOrEmpty(_text))
                _text = _textMesh.text;
        }
    }
}
