using System.Collections.Generic;
using System.Linq;

namespace SunnyTown
{
    /// <summary>
    /// A LastCardDialogue represents the dialogue text that is displayed to the user at the end of the game. 
    /// The dialogue is determined by the decisions the player has made in the game.
    /// </summary>
    public class LastCardDialogue
    {
        /// <summary>
        /// Creates a SimpleDialogue object based on the the provided PastTokens.
        /// </summary>
        /// <param name="PastTokens">PastTokens representing the users story decisions.</param>
        /// <returns>A SimpleDialogue representing the dialogue played to the user reflecting their decisions</returns>
        public static SimpleDialogue CreateFinalDialogue(Dictionary<string, string> PastTokens)
        {
            List<string> finalDialogue = new List<string>();
            List<string> goodDeeds = new List<string>();
            List<string> badDeeds = new List<string>();

            finalDialogue.Add("Mrs. Gatburg, it's your last day as Mayor!");
            string accomplishments = "You've done plenty in your time, and we're honoured to have helped you along the way. ";
            goodDeeds.Add("You've made plenty of swell decisions that did wonders for the townspeople. ");
            badDeeds.Add("You've made some not so swell decisions that on hindsight could've been done better. ");

            foreach (KeyValuePair<string, string> entry in PastTokens)
            {
                if (entry.Key.Equals("investment"))
                {
                    goodDeeds.Add("You invested in " + entry.Value + " early that has helped the town's long term growth. ");
                }
                if (entry.Key.Equals("arvio2"))
                {
                    switch (entry.Value)
                    {
                        case "yes":
                            badDeeds.Add("You permitted Arvio to set up a coal mine in the city that did damage to the city's landscape'. ");
                            break;
                        case "no":
                            goodDeeds.Add("You prevented Arvio from setting up a coal mine in the city that could damage the city's landscape. ");
                            break;
                    }
                }
                if (entry.Key.Equals("farming"))
                {
                    switch (entry.Value)
                    {
                        case "dairy":
                            badDeeds.Add("You encouraged dairy farming which is much less environmentally friendly than vegetable farming. ");
                            break;
                        case "no":
                            goodDeeds.Add("You promoted farming fruits and vegetables which is much more environmentally friendly. ");
                            break;
                    }
                }
                if (entry.Key.Equals("transport"))
                {
                    switch (entry.Value)
                    {
                        case "gas":
                            badDeeds.Add("You did not do much to solve transport which did a lot of damage to the environment. ");
                            break;
                        case "ev":
                            goodDeeds.Add("You promoted electric vehicles which has much lower emissions compared to traditional cars. ");
                            break;
                        case "public":
                            goodDeeds.Add("You build up the city's public transport which has much lower emissions compared to personal cars. ");
                            break;
                    }
                }
                if (entry.Key.Equals("refugees"))
                {
                    switch (entry.Value)
                    {
                        case "yes":
                            goodDeeds.Add("You even went out of your way to lend a helping hand to the neighbouring city in their moment of need. ");
                            break;

                    }
                }
            }

            if (goodDeeds.Count > 1)
            {
                finalDialogue.AddRange(goodDeeds);
            }

            if (badDeeds.Count > 1)
            {
                finalDialogue.AddRange(badDeeds);

            }
            finalDialogue.Add("It was a lot of fun working with you, and we hoped you learn't a lot through your time as mayor!");
            SimpleDialogue output = new SimpleDialogue(finalDialogue.ToArray(), "Advisory Board");
            return output;
        }
    }
}