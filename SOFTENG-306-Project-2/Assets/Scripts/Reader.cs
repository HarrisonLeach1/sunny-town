using System.Collections.Generic;
using System.IO;
using System;
using SimpleJSON;
using UnityEngine;

public class Reader
{
    public State RootState { get; private set; }
    public List<State> allStates { get; private set; }
    public Reader()
    {
        
	    this.parseJson(Directory.GetCurrentDirectory() + "/Assets/json/plotStates.json");
	    RootState = this.allStates[0];
	    Debug.Log("numStates: " + allStates.Count);
	    
    }

    private void parseJson(string filePath)
    {
	    List<State> result = new List<State>();

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

			    result.Add(new State(state["id"], state["dialogue"], transitionList));
		    }


	    }

	    this.allStates = result;
	    //return result;
    }

}