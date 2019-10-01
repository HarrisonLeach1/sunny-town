using System.Collections.Generic;
using System.IO;
using System;
using SimpleJSON;
using UnityEngine;

public class Reader
{

    public StoryCard RootState { get; private set; }
    public List<StoryCard> AllStates { get; private set; }

    public Reader()
    {
        
	    this.parseJson(Directory.GetCurrentDirectory() + "/Assets/json/plotStates.json");
	    RootState = this.AllStates[0];
	    Debug.Log("numStates: " + AllStates.Count);
	    
    }

    private void ParseJson(string filePath)
    {
	    List<StoryCard> result = new List<StoryCard>();


	    using (StreamReader r = new StreamReader(filePath))
	    {
		    string json = r.ReadToEnd();

		    JSONArray stateArray = SimpleJSON.JSON.Parse(json).AsArray;
		    foreach (JSONNode state in stateArray)
		    {
			    List<Transition> transitionList = new List<Transition>();
			    foreach (JSONNode transition in state["transitions"].AsArray)
			    {
				    transitionList.Add(new Transition(transition["label"], transition["state"]));
			    }

			    result.Add(new StoryCard(state["id"], state["dialogue"], transitionList));
		    }

	    }

	    this.AllStates = result;
    }

}