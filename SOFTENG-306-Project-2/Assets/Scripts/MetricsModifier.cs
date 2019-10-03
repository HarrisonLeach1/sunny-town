public class MetricsModifier
{
    private Metrics Metrics = Metrics.Instance;
    private int EnvHealthModifier { get; set; }
    private int PopHappinessModifier { get; set; }
    private int GoldModifier { get; set; }
    
    public MetricsModifier(int popHappinessModifier, int goldModifier, int envHealthModifier) {
        this.PopHappinessModifier = popHappinessModifier;
        this.GoldModifier = goldModifier;
        this.EnvHealthModifier = envHealthModifier;
    }

    public void Modify() {
        this.Metrics.UpdatePopHappiness(PopHappinessModifier);
        this.Metrics.UpdateGold(GoldModifier);
        this.Metrics.UpdateEnvHealth(EnvHealthModifier);
    }
}