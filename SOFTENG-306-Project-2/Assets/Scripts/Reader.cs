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
	    foreach (State state in allStates)
	    {
		    Debug.Log("this state's id: " + state.Id);
	    }


// This represents a binary decision tree of depth 2
//        State state4 = new State("Decision 4", new List<Transition>());
//        State state5 = new State("Decision 5", new List<Transition>());
//        Transition transition2to4 = new Transition("Go to State 4 ", state4);
//        Transition transition2to5 = new Transition("Go to State 5 ", state5);
//
//        List<Transition> transitionsFrom2 = new List<Transition>();
//        transitionsFrom2.Add(transition2to4);
//        transitionsFrom2.Add(transition2to5);
//
//        State state6 = new State("Decision 6", new List<Transition>());
//        State state7 = new State("Decision 7", new List<Transition>());
//        Transition transition3to6 = new Transition("Go to State 6 ", state6);
//        Transition transition3to7 = new Transition("Go to State 7 ", state7);
//
//        List<Transition> fourthTransitions = new List<Transition>();
//        fourthTransitions.Add(transition3to6);
//        fourthTransitions.Add(transition3to7);
//
//        State state2 = new State("Decision 2", transitionsFrom2);
//        State state3 = new State("Decision 3", fourthTransitions);
//        Transition transition1to2 = new Transition("Go to State 2 ", state2);
//        Transition transition1to3 = new Transition("Go to State 3 ", state3);
//
//        List<Transition> transitionsFrom1 = new List<Transition>();
//        transitionsFrom1.Add(transition1to2);
//        transitionsFrom1.Add(transition1to3);
//
//        RootState = new State("Decision 1", transitionsFrom1);
    }

    private void parseJson(string filePath)
    {
	    List<State> result = new List<State>();

	    using (StreamReader r = new StreamReader(filePath))
	    {
		    string json = r.ReadToEnd();
		    Debug.Log(json);

		    //JSONNode data = SimpleJSON.JSON.Parse(json);
		    JSONArray stateArray = SimpleJSON.JSON.Parse(json).AsArray;
		    foreach (JSONNode state in stateArray)
		    {
			    List<Transition> transitionList = new List<Transition>();
			    foreach (JSONNode transition in state["transitions"].AsArray)
			    {
				    //Transition t = new Transition(transition["label"], transition["state"]);
				    //transitionList.Add(t);
				    transitionList.Add(new Transition(transition["label"].ToString(), transition["state"].ToString()));

			    }

			    result.Add(new State(state["id"], state["dialogue"], transitionList));
		    }


	    }

	    this.allStates = result;
	    //return result;
    }

}