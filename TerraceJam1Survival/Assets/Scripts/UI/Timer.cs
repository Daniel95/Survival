using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    private float timer;
    private Text text;

    private void Update()
    {
        timer += Time.deltaTime;

        text.text = ((short)timer).ToString();
    }

    private void Awake()
    {
        text = GetComponent<Text>();
    }
}
