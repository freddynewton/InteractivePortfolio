using System.Threading.Tasks;
using TransitionsPlus;
using UnityEngine;

/// <summary>
/// Manages the transition screen animations, including showing and hiding transitions with smooth progress control.
/// </summary>
public class TransitionManager : MonoBehaviour
{
    [SerializeField] private TransitionAnimator transitionAnimator;

    /// <summary>
    /// Shows the transition screen over a specified duration.
    /// Gradually increases the transition progress from 0 to 1.
    /// </summary>
    /// <param name="duration">The duration, in seconds, over which the transition is shown.</param>
    public async Task ShowTransition(float duration = 2)
    {
        // Initialize transition progress at the beginning of the show transition
        transitionAnimator.progress = 0;

        // Gradually increase progress until it reaches 1
        while (transitionAnimator.progress < 1)
        {
            transitionAnimator.progress += Time.deltaTime / duration;
            await Task.Yield();  // Yield to allow other operations between frames
        }

        // Ensure progress reaches 1 at the end of the transition
        transitionAnimator.progress = 1;
    }

    /// <summary>
    /// Hides the transition screen over a specified duration.
    /// Gradually decreases the transition progress from 1 to 0.
    /// </summary>
    /// <param name="duration">The duration, in seconds, over which the transition is hidden.</param>
    public async Task HideTransition(float duration = 2)
    {
        // Initialize transition progress at the beginning of the hide transition
        transitionAnimator.progress = 1;

        // Gradually decrease progress until it reaches 0
        while (transitionAnimator.progress > 0)
        {
            transitionAnimator.progress -= Time.deltaTime / duration;
            await Task.Yield();  // Yield to allow other operations between frames
        }

        // Ensure progress reaches 0 at the end of the transition
        transitionAnimator.progress = 0;
    }

    /// <summary>
    /// Sets up the TransitionAnimator component if not already assigned.
    /// </summary>
    private void Start()
    {
        transitionAnimator ??= GetComponent<TransitionAnimator>();
    }
}
