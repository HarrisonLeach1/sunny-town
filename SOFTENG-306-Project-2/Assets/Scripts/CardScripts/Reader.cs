using System.Collections.Generic;
using System;
using SimpleJSON;
using UnityEngine;
using System.Linq;

namespace SunnyTown
{
    /// <summary>
    /// A Reader is repsonsible for reading json files and converting it into 
    /// Card and SimpleDialogue objects at runtime
    /// </summary>
    public class Reader
    {
        private static Reader instance = null;
        public PlotCard RootState { get; private set; }
        public List<SimpleDialogue> AllExpositionDialogues { get; private set; }
        public List<PlotCard> AllStoryStates { get; private set; }
        public List<Card> AllMinorStates { get; private set; }
        public List<Achievement> AllAchievements { get; private set; }
        public static Reader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Reader();
                }
                return instance;
            }
        }

        private Reader()
        {
            AllExpositionDialogues =
                this.ParseExpositionJson(Resources.Load<TextAsset>("json/expositionStates").text);
            AllStoryStates = this.ParseJson(Resources.Load<TextAsset>("json/newNewPlot").text, true)
                .Cast<PlotCard>().ToList();
            AllMinorStates = this.ParseJson(Resources.Load<TextAsset>("json/minorStates").text, false);
            AllAchievements =
                this.ParseAchievementsJson(Resources.Load<TextAsset>("json/achievements").text);
            RootState = this.AllStoryStates[0];
            Debug.Log(RootState.NPCName);
        }

        private List<Card> ParseJson(string json, bool isPlotJson)
        {
            List<Card> result = new List<Card>();

            JSONArray stateArray = SimpleJSON.JSON.Parse(json).AsArray;
            foreach (JSONNode state in stateArray)
            {
                List<string> precedingDialogue = new List<string>();

                var foundPrecedingDialogue = state["precedingDialogue"].AsArray;
                if (foundPrecedingDialogue.Count != 0)
                {
                    foreach (JSONNode dialogue in state["precedingDialogue"])
                    {
                        precedingDialogue.Add(dialogue);
                    }
                }

                List<Transition> optionList = new List<Transition>();

                JSONNode transitions = isPlotJson ? state["transitions"].AsArray : state["options"];

                foreach (JSONNode transition in transitions)
                {

                    int popHappinessModifier = 0;
                    int goldModifier = 0;
                    int envHealthModifier = 0;
                    string tokenKey = "";
                    string tokenValue = "";
                    string additionalState = "";

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

                    if (transition["token"])
                    {
                        tokenKey = transition["token"];
                        tokenValue = transition[tokenKey];
                    }

                    if (transition["additionalState"])
                    {
                        additionalState = transition["additionalState"];
                    }

                    MetricsModifier metricsModifier =
                        new MetricsModifier(popHappinessModifier, goldModifier, envHealthModifier);

                    if (state["sliderType"])
                    {
                        optionList.Add(new SliderTransition(transition["feedback"], transition["npcName"],
                            metricsModifier, transition["hasAnimation"], transition["buildingName"],
                            transition["threshold"]));
                    }
                    else
                    {
                        optionList.Add(new Transition(transition["feedback"], transition["npcName"],
                            metricsModifier, transition["hasAnimation"], transition["buildingName"],
                            transition["label"], transition["state"], tokenKey, tokenValue, additionalState));
                    }
                }


                if (isPlotJson)
                {
                    String name = "";
                    if (state["name"])
                    {
                        name = state["name"];
                    }
                    result.Add(new PlotCard(state["id"], precedingDialogue.ToArray<string>(), name, state["question"], optionList));

                }
                else if (state["sliderType"])
                {
                    result.Add(new SliderCard(precedingDialogue.ToArray<string>(), state["name"], state["question"], optionList.Cast<SliderTransition>().ToList(), state["maxValue"], state["minValue"]));
                }
                else
                {
                    result.Add(new MinorCard(precedingDialogue.ToArray<string>(), state["name"], state["question"], optionList));
                }
            }

            return result;

        }

        private List<SimpleDialogue> ParseExpositionJson(string json)
        {
            List<SimpleDialogue> result = new List<SimpleDialogue>();

            JSONArray expositionArray = SimpleJSON.JSON.Parse(json).AsArray;
            foreach (JSONNode exposition in expositionArray)
            {
                List<string> dialogueList = new List<string>();

                foreach (JSONNode dialogue in exposition["dialogue"])
                {
                    dialogueList.Add(dialogue);
                }

                SimpleDialogue sd = new SimpleDialogue(dialogueList.ToArray(), exposition["name"]);
                result.Add(sd);
            }


            return result;
        }

        private List<Achievement> ParseAchievementsJson(string json)
        {
            List<Achievement> result = new List<Achievement>();

            JSONArray achievementArray = SimpleJSON.JSON.Parse(json).AsArray;
            foreach (JSONNode achievement in achievementArray)
            {

                Achievement ach = new Achievement(
                    achievement["name"], achievement["description"], achievement["imageUrl"]);
                result.Add(ach);
            }

            return result;
        }
    }
}