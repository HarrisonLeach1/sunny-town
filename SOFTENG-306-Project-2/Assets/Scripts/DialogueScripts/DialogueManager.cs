using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Initial Dialogue implementation based on code from: 
// https://github.com/Brackeys/Dialogue-System
public class DialogueManager : MonoBehaviour
{ 
    public Animator simpleDialogueViewAnimator;
    public Animator binaryOptionViewAnimator;
    public Animator sliderOptionViewAnimator;
    public Animator animationProgressAnimator;

    public SimpleDialogueView simpleDialogueView;
    public BinaryOptionDialogueView binaryOptionDialogueView;
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
    
    public void ShowAnimationProgress(float seconds)
    {
        animationProgressAnimator.SetBool("IsVisible", false);
        if (progressAnimationCoroutine != null)
        {
            StopCoroutine(progressAnimationCoroutine);
        }
        animationProgressDialgoueView.SetupProgressbar(seconds);
        animationProgressAnimator.SetBool("IsVisible", true);
        progressAnimationCoroutine = StartCoroutine(AnimationWait(seconds));
    }

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
        Action onEndOfStatements = () =>
        {
            simpleDialogueViewAnimator.SetTrigger("ToggleVisibilitySmooth");
            onClosed();
        };
        StartSimpleDialogue(dialogue, onEndOfStatements);
    }

    public void StartBinaryOptionDialogue(BinaryOptionDialogue dialogue, Action<int> onOptionPressed)
    {
        Action<int> handleButtonPressed = num =>
        {
            onOptionPressed(num);
            binaryOptionViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        };

        if (dialogue.PrecedingDialogue.Statements.Length != 0)
        {
            StartSimpleDialogue(dialogue.PrecedingDialogue, () => {
                binaryOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                simpleDialogueViewAnimator.SetTrigger("ToggleVisibilityInstant");
                binaryOptionViewAnimator.SetTrigger("ToggleVisibilityInstant");
            });
        }
        else
        {
            binaryOptionDialogueView.SetContent(dialogue, handleButtonPressed);
            binaryOptionViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        }
    }

    public void StartSliderOptionDialogue(SliderOptionDialogue dialogue, Action<int> onValueConfirmed)
    {
        Action<int> handleButtonPressed = value =>
        {
            onValueConfirmed(value);
            sliderOptionViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        };

        if (dialogue.PrecedingDialogue.Statements.Length != 0)
        {
            StartSimpleDialogue(dialogue.PrecedingDialogue, () => {
                sliderOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                simpleDialogueViewAnimator.SetTrigger("ToggleVisibilityInstant");
                sliderOptionViewAnimator.SetTrigger("ToggleVisibilityInstant");
            });
        }
        else
        {
            sliderOptionDialogueView.SetContent(dialogue, handleButtonPressed);
            sliderOptionViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        }
    }

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

    private void StartSimpleDialogue(SimpleDialogue dialogue, Action onEndOfStatements)
    {
        this.onEndOfStatements = onEndOfStatements;
        simpleDialogueViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        statements.Clear();

        simpleDialogueView.npcNameText.text = dialogue.Name;
        foreach (string statement in dialogue.Statements)
        {
            statements.Enqueue(statement);
        }

        DisplayNextStatement();
    }
}
