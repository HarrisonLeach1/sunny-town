using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetricManager : MonoBehaviour
{
    public static MetricManager Instance { get; private set; }

    [SerializeField]
    private GameObject metricPrefab;
    private GameObject metricsView;

    public int PopHappiness { get; private set; }
    public int Gold { get; private set; }
    public int EnvHealth { get; private set; }
    private const int START_VALUE = 50;
    private const int MAX_VALUE = 100;
    private const int MIN_VALUE = 0;


    private MetricManager()
    {
        this.PopHappiness = START_VALUE;
        this.Gold = START_VALUE;
        this.EnvHealth = START_VALUE;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RenderMetrics();
    }

    public void RenderMetrics()
    {
        Debug.Log("Render metrics called: " + PopHappiness + Gold + EnvHealth);
        Destroy(metricsView);
        metricsView = Instantiate(metricPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        var money = metricsView.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var happiness = metricsView.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        var environment = metricsView.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        money.text = Gold.ToString();
        happiness.text = PopHappiness.ToString();
        environment.text = EnvHealth.ToString();

        var parentObject = GameObject.Find("MetricPanel");
        metricsView.transform.SetParent(parentObject.transform, false);
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
            
            //TODO: Probably will change later, this changes scenes to end game screen upon losing mats
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            
            //TODO: Probably will change later, this changes scenes to end game screen upon losing mats
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            
            //TODO: Probably will change later, this changes scenes to end game screen upon losing mats
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}