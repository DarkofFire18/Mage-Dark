using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progressSlider; // Fortschrittsleiste
    public float changeAmount = 10f; // Wie stark sich die Leiste verändert

    void Start()
    {
        if (progressSlider == null)
            Debug.LogError("Slider nicht zugewiesen!");
    }

    public void IncreaseProgress()
    {
        progressSlider.value = Mathf.Clamp(progressSlider.value + changeAmount, progressSlider.minValue, progressSlider.maxValue);
        Debug.Log("Fortschritt erhöht: " + progressSlider.value);
    }

    public void DecreaseProgress()
    {
        progressSlider.value = Mathf.Clamp(progressSlider.value - changeAmount, progressSlider.minValue, progressSlider.maxValue);
        Debug.Log("Fortschritt verringert: " + progressSlider.value);
    }
}
