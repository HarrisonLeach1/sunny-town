using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    public class CampaignManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject campaignPanel;
        [SerializeField]
        private TMP_Dropdown titleDropdown;
        [SerializeField]
        private ToggleGroup imageToggleGroup;
        [SerializeField]
        private TMP_Dropdown firstTextDropdown;
        [SerializeField]
        private TMP_Dropdown secondTextDropdown;
        [SerializeField]
        private TMP_Dropdown thirdTextDropdown;

        private int popHappinessScore = 0;
        private int goldScore = 0;
        private int environmentalScore = 0;
        private CampaignWeightings currentWeightings;

        /// <summary>
        /// Submits the campaign form to be processed and applied to the MetricManager, should be triggered by a button
        /// when the user finishes creating the campaign.
        /// </summary>
        public void OnSubmit()
        {
            MetricManager.Instance.RenderMetrics();
            campaignPanel.gameObject.SetActive(false);
            // Reset the weightings on every campaign
            popHappinessScore = 0;
            goldScore = 0;
            environmentalScore = 0;

            CalculateDropdownScores();
            CalculateImageScore();
            UpdateMetricManagerWeightings();
            SimpleDialogue dialgoue = GenerateExplanatoryDialgoue();

            Action onDialogueClose = () =>
            {
                CardManager.Instance.SetState(CardManager.GameState.WaitingForEvents);
            };

            DialogueManager.Instance.StartExplanatoryDialogue(dialgoue, onDialogueClose);

            Debug.Log("Scores from campaign: money: " + goldScore + " pop happiness: " + popHappinessScore + " env: " + environmentalScore);

        }

        /// <summary>
        /// Opens the campaign panel if the game is in the correct state to do so. Additionally, provides a
        /// small explanatory dialogue of the campaign mechanic. Also, oves the game into a paused state.
        /// </summary>
        public void StartCampaignDialogue()
        {
            CardManager.Instance.SetState(CardManager.GameState.GamePaused);
            Action onClosed = () =>
            {
                campaignPanel.gameObject.SetActive(true);
            };
            SimpleDialogue dialogue = new SimpleDialogue(new string[2] {
                    "Hello Mrs. Gatberg, to keep your position as mayor we must launch a new campaign. We need your guidance in designing the new campaign poster.",
                    "Be careful what you choose, as it will shape the public's opinion on your future decisions." },
                "Advisory Board");
            DialogueManager.Instance.StartExplanatoryDialogue(dialogue, onClosed);
            Debug.Log("Successfully opened campaign");
        }

        /// <summary>
        /// Based on the selected image, updates the score for a metric.
        /// </summary>
        private void CalculateImageScore()
        {
            Toggle[] toggles = GetComponentsInChildren<Toggle>();
            string chosenToggleName = "";
            foreach (var t in toggles)
            {
                if (t.isOn)
                    chosenToggleName = t.name;
            }
            Debug.Log("chosen toggle name: " + chosenToggleName);
            switch (chosenToggleName)
            {
                case ("PopulationToggle"):
                    popHappinessScore++;
                    break;
                case ("EnvironmentToggle"):
                    environmentalScore++;
                    break;
                default:
                    goldScore++;
                    break;
            }
        }


        /// <summary>
        /// Based on the selected dropdowns, updates the scores for metrics.
        /// </summary>
        private void CalculateDropdownScores()
        {
            var dropdowns = new TMP_Dropdown[4] { titleDropdown, firstTextDropdown, secondTextDropdown, thirdTextDropdown };
            foreach (TMP_Dropdown dropdown in dropdowns)
            {
                switch (dropdown.value)
                {
                    case (0):
                        goldScore++;
                        break;
                    case (1):
                        environmentalScore++;
                        break;
                    case (2):
                        popHappinessScore++;
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the MetricManager of the weightings from the campaign such that decisions affecting the most heavily weighted metric
        /// also impact population happiness. Currently only the most heavily weighted metric is used. And the player must receive at least a score 
        /// of three for weightings to be applied.
        /// </summary>
        private void UpdateMetricManagerWeightings()
        {
            var campaignWeightingsArray = new float[3] { 0, 0, 0 };
            var scoresList = new List<int>(new int[3] { popHappinessScore, goldScore, environmentalScore });
            var maxScore = scoresList.Max();
            if (maxScore >= 3)
            {
                var maxScoreIndex = scoresList.IndexOf(maxScore);
                Debug.Log("Max Score: " + maxScore);

                campaignWeightingsArray[maxScoreIndex] = maxScore / 10f;
            }
            currentWeightings = new CampaignWeightings(campaignWeightingsArray[0], campaignWeightingsArray[1], campaignWeightingsArray[2]);
            MetricManager.Instance.campaignWeightings = currentWeightings;
        }

        /// <summary>
        /// Returns a SimplaDialogue to explain what the current weightings mean.
        /// </summary>
        /// <returns>SimpleDialogue that can displayed to the user</returns>
        private SimpleDialogue GenerateExplanatoryDialgoue()
        {
            string[] statements;
            if (currentWeightings.Happiness > 0)
            {
                statements = new string[2] { "Your campaign has convinced the citisens that you are a woman of the people", "They will now react more strongly to decisions that affect them" };
            }
            else if (currentWeightings.Gold > 0)
            {
                statements = new string[2] { "Your campaign has convinced the citisens that you will create a thriving economy in Sunnytown", "They will now react to decisions that impact the economy" };
            }
            else if (currentWeightings.EnvHealth > 0)
            {
                statements = new string[2] { "Your campaign has convinced the citisens that environmental health is of utmost importance", "They will now react to decisions that impact the environment" };
            }
            else
            {
                statements = new string[1] { "Your campaign sent mixed messages to the public, and has left the public's opinion unchanged" };
            }
            return new SimpleDialogue(statements, "Advisory Board");
        }
    }
}