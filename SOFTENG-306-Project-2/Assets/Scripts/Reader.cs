using System.Collections.Generic;

public class Reader
{
    public StoryCard RootState { get; private set; }
    public Reader()
    {
        // This represents a binary decision tree of depth 2

        StoryCard state4 = new StoryCard("Decision 4", new List<Transition>());
        StoryCard state5 = new StoryCard("Decision 5", new List<Transition>());
        Transition transition2to4 = new Transition("Go to State 4 ", state4);
        Transition transition2to5 = new Transition("Go to State 5 ", state5);

        List<Transition> transitionsFrom2 = new List<Transition>();
        transitionsFrom2.Add(transition2to4);
        transitionsFrom2.Add(transition2to5);

        StoryCard state6 = new StoryCard("Decision 6", new List<Transition>());
        StoryCard state7 = new StoryCard("Decision 7", new List<Transition>());
        Transition transition3to6 = new Transition("Go to State 6 ", state6);
        Transition transition3to7 = new Transition("Go to State 7 ", state7);

        List<Transition> fourthTransitions = new List<Transition>();
        fourthTransitions.Add(transition3to6);
        fourthTransitions.Add(transition3to7);

        StoryCard state2 = new StoryCard("Decision 2", transitionsFrom2);
        StoryCard state3 = new StoryCard("Decision 3", fourthTransitions);
        Transition transition1to2 = new Transition("Go to State 2 ", state2);
        Transition transition1to3 = new Transition("Go to State 3 ", state3);

        List<Transition> transitionsFrom1 = new List<Transition>();
        transitionsFrom1.Add(transition1to2);
        transitionsFrom1.Add(transition1to3);

        RootState = new StoryCard("Decision 1", transitionsFrom1);
    }

}