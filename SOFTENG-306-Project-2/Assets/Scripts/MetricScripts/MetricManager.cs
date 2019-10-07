using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MetricManager : MonoBehaviour
{
    public static MetricManager Instance { get; private set; }

    [SerializeField]
    private GameObject metricPrefab;
    private GameObject metricsView;

    public int PopHappiness { get; private set; }
    public int Gold { get; private set; }
    public int EnvHealth { get; private set; }

    private int PrevPopHappiness { get; set; }
    private int PrevGold { get; set; }
    private int PrevEnvHealth { get; set; }

    private const int START_VALUE = 50;
    private const int MAX_VALUE = 100;
    private const int MIN_VALUE = 0;


    private MetricManager()
    {
        this.PopHappiness = START_VALUE;
        this.Gold = START_VALUE;
        this.EnvHealth = START_VALUE;
        this.PrevGold = START_VALUE;
        this.PrevEnvHealth = START_VALUE;
        this.PrevPopHappiness = START_VALUE;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        metricsView = Instantiate(metricPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        RenderMetrics();
    }

    public void RenderMetrics()
    {
        var money = metricsView.transform.GetChild(0).GetComponent<Slider>();
        var happiness = metricsView.transform.GetChild(1).GetComponent<Slider>();
        var environment = metricsView.transform.GetChild(2).GetComponent<Slider>();

        StartCoroutine(AnimateMetric(money, PrevGold, Gold));
        StartCoroutine(AnimateMetric(happiness, PrevPopHappiness, PopHappiness));
        StartCoroutine(AnimateMetric(environment, PrevEnvHealth, EnvHealth));

        PrevGold = Gold;
        PrevEnvHealth = EnvHealth;
        PrevPopHappiness = PopHappiness;

        var parentObject = GameObject.Find("MetricPanel");
        metricsView.transform.SetParent(parentObject.transform, false);
    }

    public int GetScore()
    {
        return (int)(0.5 * EnvHealth + 0.25 * Gold + 0.25 * PopHappiness);
    }

    IEnumerator AnimateMetric(Slider metricBar, int oldValue, int newValue)
    {
        Image metricBarFill = metricBar.transform.GetChild(2).GetChild(0).GetComponent<Image>();

        int tempValue = oldValue;
        if (tempValue == newValue)
        {
            metricBarFill.color = new Color32(75, 75, 75, 255);
        }
        while (tempValue != newValue)
        {
            yield return null;
            if (tempValue < newValue)
            {
                tempValue++;
                metricBarFill.color = Color.green;
            }
            else if (tempValue > newValue)
            {
                tempValue--;
                metricBarFill.color = Color.red;
            }

            metricBar.value = (float)tempValue / MAX_VALUE;
        }

        metricBar.value = (float)newValue / MAX_VALUE;
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

            SimpleDialogue endGameDialogue = new SimpleDialogue(new string[1] { "Lost pop happiness" }, "");
            CardManager.Instance.QueueEndDialogue(endGameDialogue);
        }
    }

    public void UpdateGold(int value)
    {
        Debug.Log("called update gold: " + value);

        this.Gold += value;
        if (this.Gold > MAX_VALUE)
        {
            this.Gold = MAX_VALUE;
        }

        if (this.Gold < MIN_VALUE)
        {
            this.Gold = MIN_VALUE;

            //TODO: Probably will change later, this changes scenes to end game screen upon losing mats
            SimpleDialogue endGameDialogue = new SimpleDialogue(new string[1] { "Lost gold" }, "");
            CardManager.Instance.QueueEndDialogue(endGameDialogue);
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
            SimpleDialogue endGameDialogue = new SimpleDialogue(new string[1] { "Lost env health" }, "");

            CardManager.Instance.QueueEndDialogue(endGameDialogue);
        }
    }
}