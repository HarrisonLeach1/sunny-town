using System.Collections.Generic;
using System.IO;
using System;
using SimpleJSON;
using UnityEngine;

public class Reader
{

    public PlotCard RootState { get; private set; }
    public List<PlotCard> AllStoryStates { get; private set; }
    public List<MinorCard> AllMinorStates { get; private set; }

    public Reader()
    {

        this.ParseStoryJson(Directory.GetCurrentDirectory() + "/Assets/json/plotStates.json");
        this.ParseMinorCardJson(Directory.GetCurrentDirectory() + "/Assets/json/minorStates.json");
        RootState = this.AllStoryStates[0];
    }

    private void ParseStoryJson(string filePath)
    {
        List<PlotCard> result = new List<PlotCard>();


        using (StreamReader r = new StreamReader(filePath))
        {
            string json = r.ReadToEnd();

            JSONArray stateArray = SimpleJSON.JSON.Parse(json).AsArray;
            foreach (JSONNode state in stateArray)
            {
                List<Transition> transitionList = new List<Transition>();
                foreach (JSONNode transition in state["transitions"].AsArray)
                {
                    Dictionary<string, int> metrics = new Dictionary<string, int>();
                    foreach (JSONNode metricsArray in transition["metrics"].AsArray)
                    {
                        metrics.Add("money", metricsArray["money"]);
                        metrics.Add("happiness", metricsArray["happiness"]);
                        metrics.Add("environment", metricsArray["environment"]);
                    }
                    transitionList.Add(new Transition(transition["label"], transition["feedback"], metrics, transition["state"]));
                }

                result.Add(new PlotCard(state["id"], state["dialogue"], transitionList));
            }

        }

        this.AllStoryStates = result;
    }

    private void ParseMinorCardJson(string filePath)
    {
        List<MinorCard> result = new List<MinorCard>();


        using (StreamReader r = new StreamReader(filePath))
        {
            string json = r.ReadToEnd();

            JSONArray decisionArray = SimpleJSON.JSON.Parse(json).AsArray;
            foreach (JSONNode decision in decisionArray)
            {
                List<Option> optionList = new List<Option>();
                foreach (JSONNode option in decision["options"].AsArray)
                {
                    optionList.Add(new Option(option["label"]));
                }

                result.Add(new MinorCard(decision["dialogue"], optionList));
            }

        }

        this.AllMinorStates = result;
    }

}