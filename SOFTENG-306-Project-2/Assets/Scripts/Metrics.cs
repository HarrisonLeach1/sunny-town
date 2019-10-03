public class Metrics
{
    private static Metrics instance = null;
    public int PopHappiness { get; private set; }
    public int Gold { get; private set; }
    public int EnvHealth { get; private set; }
    private const int START_VALUE = 50;
    private const int MAX_VALUE = 100;
    private const int MIN_VALUE = 0;


    private Metrics()
    {
        this.PopHappiness = START_VALUE;
        this.Gold = START_VALUE;
        this.EnvHealth = START_VALUE;
    }

    public static Metrics Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Metrics();
            }

            return instance;
        }
    }

    public void UpdatePopHappiness(int value)
    {
        this.PopHappiness += value;
        if (this.PopHappiness > MAX_VALUE)
        {
            this.PopHappiness = MAX_VALUE;
        }

        if (this.PopHappiness < MIN_VALUE)
        {
            this.PopHappiness = MIN_VALUE;
        }
    }

    public void UpdateGold(int value)
    {
        this.Gold += value;
        if (this.Gold > MAX_VALUE)
        {
            this.Gold = MAX_VALUE;
        }

        if (this.Gold < MIN_VALUE)
        {
            this.Gold = MIN_VALUE;
        }
    }

    public void UpdateEnvHealth(int value)
    {
        this.EnvHealth += value;
        if (this.EnvHealth > MAX_VALUE)
        {
            this.EnvHealth = MAX_VALUE;
        }

        if (this.EnvHealth < MIN_VALUE)
        {
            this.EnvHealth = MIN_VALUE;
        }
    }
}