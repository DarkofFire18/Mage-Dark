using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider progressBar;

    void Start()
    {
        if (progressBar == null)
        {
            Debug.LogError("Kein Slider-Element zugewiesen!");
        }
    }

    public void ChangeProgress(float amount)
    {
        if (progressBar != null)
        {
            progressBar.value = Mathf.Clamp(progressBar.value + amount, progressBar.minValue, progressBar.maxValue);
        }
    }
}
