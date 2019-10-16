using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SunnyTown
{
    /// <summary>
    /// The DialogueManager is a singleton that handles displaying views of Dialogue 
    /// objects to the user. 
    /// </summary>
    // Initial Dialogue implementation based on code from: 
    // https://github.com/Brackeys/Dialogue-System
    public class DialogueManager : MonoBehaviour
    {
        public Animator simpleDialogueViewAnimator;
        public Animator binaryOptionViewAnimator;
        public Animator sliderOptionViewAnimator;
        public Animator animationProgressAnimator;

        public SimpleDialogueView simpleDialogueView;
        public OptionDialogueView binaryOptionDialogueView;
        public SliderOptionDialogueView sliderOptionDialogueView;
        public AnimationProgressDialgoueView animationProgressDialgoueView;

        private Queue<string> statements = new Queue<string>();
        private Action onEndOfStatements;
        private Coroutine progressAnimationCoroutine;

        public static DialogueManager Instance { get; private set; }

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

        /// <summary>
        /// Displays a progress bar to the user for the specified number of seconds
        /// </summary>
        /// <param name="seconds">The number of seconds to display progress for</param>
        public void ShowAnimationProgress(float seconds)
        {
            animationProgressAnimator.SetBool("IsVisible", false);
            if (progressAnimationCoroutine != null)
            {
                StopCoroutine(progressAnimationCoroutine);
            }
            animationProgressAnimator.SetBool("IsVisible", true);
            // need offset here otherwise the progress bar will stay visible
            float offset = 0.1f;
            progressAnimationCoroutine = StartCoroutine(AnimationWait(seconds - offset));
        }

        /// <summary>
        /// A Couroutine which waits for the animation and updates the slider progress
        /// bar
        /// </summary>
        /// <param name="seconds">The number of seconds to update the progress bar for</param>
        private IEnumerator AnimationWait(float seconds)
        {
            animationProgressDialgoueView.slider.value = 0;
            float timeProgressed = 0;
            while (timeProgressed < seconds)
            {
                timeProgressed += Time.deltaTime;
                animationProgressDialgoueView.slider.value = timeProgressed / seconds;
                yield return null;
            }
            animationProgressAnimator.SetBool("IsVisible", false);
        }

        /// <summary>
        /// Displays purely explanatory dialogue. This means the only interactivity available is
        /// pressing the "Continue" button. i.e. no decisions to be made.
        /// </summary>
        /// <param name="dialogue"></param>
        /// <param name="onClosed"></param>
        public void StartExplanatoryDialogue(SimpleDialogue dialogue, Action onClosed)
        {
            Action onEndOfStatement = () =>
            {
                simpleDialogueViewAnimator.SetBool("InstantTransition", false);
                simpleDialogueViewAnimator.SetBool("IsVisible", false);
                onClosed();
            };
            StartSimpleDialogue(dialogue, onEndOfStatement);
        }

        /// <summary>
        /// Displays a binary choice dialogue to the user, allowing them to interact with it
        /// </summary>
        /// <param name="dialogue">The BinaryOptionDialogue object which contains the information for the view</param>
        /// <param name="onOptionPressed">A callback which is used when the user interacts with the card</param>
        public void StartBinaryOptionDialogue(OptionDialogue dialogue, Action<int> onOptionPressed)
        {
            Debug.Log("Got dialogue: " + dialogue.Question + dialogue.PrecedingDialogue);
            Action<int> handleButtonPressed = num =>
            {
                onOptionPressed(num);
                binaryOptionViewAnimator.SetBool("InstantTransition", false);
                binaryOptionViewAnimator.SetBool("IsVisible", false);
            };

            //TODO handle if absent preceding dialogue
            if (dialogue.PrecedingDialogue.Statements.Length != 0)
            {
                StartSimpleDialogue(dialogue.PrecedingDialogue, () =>
                {
                    binaryOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                    simpleDialogueViewAnimator.SetBool("InstantTransition", true);
                    simpleDialogueViewAnimator.SetBool("IsVisible", false);
                    binaryOptionViewAnimator.SetBool("InstantTransition", true);
                    binaryOptionViewAnimator.SetBool("IsVisible", true);
                });
            }
            else
            {
                binaryOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                binaryOptionViewAnimator.SetBool("InstantTransition", false);
                binaryOptionViewAnimator.SetBool("IsVisible", true);
            }
        }

        /// <summary>
        /// Displays a slider option dialogue which, allowing the user to interact with it
        /// </summary>
        /// <param name="dialogue">The SliderOptionDialogue option which contains the information to be displayed on the view</param>
        /// <param name="onValueConfirmed">The callback which is invoked when the user confirms the value</param>
        public void StartSliderOptionDialogue(SliderOptionDialogue dialogue, Action<int> onValueConfirmed)
        {
            Action<int> handleButtonPressed = value =>
            {
                onValueConfirmed(value);
                sliderOptionViewAnimator.SetBool("InstantTransition", false);
                sliderOptionViewAnimator.SetBool("IsVisible", false);
            };

            if (dialogue.PrecedingDialogue.Statements.Length != 0)
            {
                StartSimpleDialogue(dialogue.PrecedingDialogue, () =>
                {
                    sliderOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                    simpleDialogueViewAnimator.SetBool("InstantTransition", true);
                    simpleDialogueViewAnimator.SetBool("IsVisible", false);
                    sliderOptionViewAnimator.SetBool("InstantTransition", true);
                    sliderOptionViewAnimator.SetBool("IsVisible", true);
                });
            }
            else
            {
                sliderOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                sliderOptionViewAnimator.SetBool("InstantTransition", false);
                sliderOptionViewAnimator.SetBool("IsVisible", true);
            }
        }

        /// <summary>
        /// Displays the next statement in a simple dialogue, and animates its writing
        /// </summary>
        public void DisplayNextStatement()
        {
            if (statements.Count == 0)
            {
                onEndOfStatements();
                return;
            }
            var statement = statements.Dequeue();
            StopAllCoroutines();
            StartCoroutine(simpleDialogueView.TypeSentence(statement));
        }

        private void StartSimpleDialogue(SimpleDialogue dialogue, Action onEndOfStatement)
        {
            simpleDialogueView.SetContent(dialogue);
            this.onEndOfStatements = onEndOfStatement;
            simpleDialogueViewAnimator.SetBool("InstantTransition", false);
            simpleDialogueViewAnimator.SetBool("IsVisible", true);
            statements.Clear();

            simpleDialogueView.npcNameText.text = dialogue.Name;
            foreach (string statement in dialogue.Statements)
            {
                statements.Enqueue(statement);
            }

            DisplayNextStatement();
        }
    }
}
