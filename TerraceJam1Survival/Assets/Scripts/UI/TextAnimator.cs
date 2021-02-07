using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private Text text;

    private string textString;

    private void Awake()
    {
        textString = text.text;
        text.text = string.Empty;
    }

    private void Update()
    {
        
    }
}
