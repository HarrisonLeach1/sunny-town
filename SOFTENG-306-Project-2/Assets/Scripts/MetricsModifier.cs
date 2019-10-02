using UnityEngine;

public class MetricsModifier
{    private int EnvHealthModifier { get; set; }
    private int PopHappinessModifier { get; set; }
    private int GoldModifier { get; set; }
    
    public MetricsModifier(int popHappinessModifier, int goldModifier, int envHealthModifier) {
        this.PopHappinessModifier = popHappinessModifier;
        this.GoldModifier = goldModifier;
        this.EnvHealthModifier = envHealthModifier;
        Debug.Log("created" + EnvHealthModifier + PopHappinessModifier + GoldModifier);

    }

    public void Modify() {
        Debug.Log("Modifying" + EnvHealthModifier + PopHappinessModifier + GoldModifier);
        MetricManager.Instance.UpdatePopHappiness(PopHappinessModifier);
        MetricManager.Instance.UpdateGold(GoldModifier);
        MetricManager.Instance.UpdateEnvHealth(EnvHealthModifier);
    }
}