using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider progressBar;
    public MaterialItem currentMaterial;

    void Start()
    {
        if (progressBar == null)
        {
            Debug.LogError("Kein Slider-Element zugewiesen!");
        }
    }

    void Update()
    {
        // Progressbar nur anzeigen, wenn ein Material existiert und auf dem Amboss liegt
        if (currentMaterial != null && currentMaterial.isOnAnvil)
        {
            progressBar.gameObject.SetActive(true);
        }
        else
        {
            progressBar.gameObject.SetActive(false);
        }
    }

    // Setzt den Fortschritt, aber nur wenn das Material heiß genug ist (mind. 80 Grad)
    public void SetProgress(float value)
    {
        if (currentMaterial != null && currentMaterial.isOnAnvil)
        {
            if (currentMaterial.heatLevel >= 80)
            {
                progressBar.value = Mathf.Clamp(value, progressBar.minValue, progressBar.maxValue);
                currentMaterial.forgingProgress = progressBar.value;
            }
            else
            {
                Debug.LogWarning("Das Material ist noch nicht heiß genug (mind. 80 Grad erforderlich)!");
            }
        }
        else
        {
            Debug.LogWarning("Kein Material auf dem Amboss ausgewählt oder es liegt nicht auf dem Amboss!");
        }
    }

    // Erhöht den Fortschritt, aber nur wenn das Material heiß genug ist
    public void IncreaseProgress(float amount)
    {
        if (currentMaterial != null && currentMaterial.isOnAnvil)
        {
            if (currentMaterial.heatLevel >= 80)
            {
                SetProgress(progressBar.value + amount);
            }
            else
            {
                Debug.LogWarning("Das Material ist noch nicht heiß genug, um den Fortschritt zu erhöhen!");
            }
        }
    }

    // Verringert den Fortschritt, aber nur wenn das Material heiß genug ist
    public void DecreaseProgress(float amount)
    {
        if (currentMaterial != null && currentMaterial.isOnAnvil)
        {
            if (currentMaterial.heatLevel >= 80)
            {
                SetProgress(progressBar.value - amount);
            }
            else
            {
                Debug.LogWarning("Das Material ist noch nicht heiß genug, um den Fortschritt zu verringern!");
            }
        }
    }
}
