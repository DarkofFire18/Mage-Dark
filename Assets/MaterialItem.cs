using UnityEngine;

public class MaterialItem : MonoBehaviour
{
    public string materialName;
    public float heatLevel = 0f;       // Starttemperatur
    public float maxHeat = 100f;       // Maximaltemperatur
    public float heatIncreaseRate = 5f;// Wärmezufuhr pro Sekunde
    public float coolDownRate = 2f;    // Abkühlrate pro Sekunde
    private bool isInOven = false;     // Status, ob Material im Ofen ist
    private bool isMelted = false;     // Falls das Material geschmolzen ist

    // Neue Eigenschaft zum Speichern des Fortschritts
    public float forgingProgress = 0f;

    // Neue Eigenschaft, ob das Material auf dem Amboss liegt
    public bool isOnAnvil = false;

    void Update()
    {
        if (isMelted) return; // Falls das Material bereits geschmolzen ist, nichts mehr tun

        if (isInOven)
        {
            // Temperatur erhöhen
            HeatUp(Time.deltaTime * heatIncreaseRate);
        }
        else
        {
            // Temperatur langsam abkühlen
            CoolDown(Time.deltaTime * coolDownRate);
        }
    }

    public void HeatUp(float amount)
    {
        heatLevel = Mathf.Clamp(heatLevel + amount, 0, maxHeat);
        Debug.Log($"{materialName} erwärmt sich: {heatLevel}°C");

        if (heatLevel >= maxHeat)
        {
            Melt();
        }
    }

    public void CoolDown(float amount)
    {
        heatLevel = Mathf.Clamp(heatLevel - amount, 0, maxHeat);
    }

    public void SetInOven(bool inOven)
    {
        isInOven = inOven;
    }

    private void Melt()
    {
        isMelted = true;
        Debug.Log($"{materialName} ist geschmolzen!");
        Destroy(gameObject); // Entfernt das Material aus der Szene
    }
}
