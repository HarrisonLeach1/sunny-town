using System.Collections.Generic;
using System.IO;
using System;
using SimpleJSON;
using UnityEngine;
using System.Linq;

public class Reader
{

    public PlotCard RootState { get; private set; }
    public List<PlotCard> AllStoryStates { get; private set; }
    public List<MinorCard> AllMinorStates { get; private set; }

    public Reader()
    {

        AllStoryStates = this.ParseJson(Directory.GetCurrentDirectory() + "/Assets/json/plotStates.json", true).Cast<PlotCard>().ToList();
        AllMinorStates = this.ParseJson(Directory.GetCurrentDirectory() + "/Assets/json/minorStates.json", false).Cast<MinorCard>().ToList(); ;
        RootState = this.AllStoryStates[0];
    }

    private List<Card> ParseJson(string filePath, bool isPlotJson)
    {
        List<Card> result = new List<Card>();

        using (StreamReader r = new StreamReader(filePath))
        {
            string json = r.ReadToEnd();

            JSONArray stateArray = SimpleJSON.JSON.Parse(json).AsArray;
            foreach (JSONNode state in stateArray)
            {
                List<Transition> optionList = new List<Transition>();

                JSONNode transitions = isPlotJson ? state["transitions"].AsArray : state["options"];

                foreach (JSONNode transition in transitions)
                {
                    int popHappinessModifier = 0;
                    int goldModifier = 0;
                    int envHealthModifier = 0;
                    
                    if (transition["happiness"])
                    {
                        popHappinessModifier = transition["happiness"];
                    }

                    if (transition["money"])
                    {
                        goldModifier = transition["money"];
                    }

                    if (transition["environment"])
                    {
                        envHealthModifier = transition["environment"];
                    }
                    
                    MetricsModifier metricsModifier = new MetricsModifier(popHappinessModifier, goldModifier, envHealthModifier);
                    optionList.Add(new Transition(transition["label"], transition["feedback"], metricsModifier, transition["state"]));
                }

                if (isPlotJson)
                {
                    Debug.Log("Adding plot options: " + optionList.Count);
                    result.Add(new PlotCard(state["id"], state["dialogue"], optionList));

                }
                else
                {
                    Debug.Log("Adding minor options: " + optionList.Count);
                    result.Add(new MinorCard(state["dialogue"], optionList));
                }
            }

        }

        return result;
    }
}