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
        transitionAnimator.profile.invert = false;
        transitionAnimator.Play();

        while (transitionAnimator.progress < 1)
        {
            await Task.Yield();  // Yield to allow other operations between frames
        }
    }

    /// <summary>
    /// Hides the transition screen over a specified duration.
    /// Gradually decreases the transition progress from 1 to 0.
    /// </summary>
    /// <param name="duration">The duration, in seconds, over which the transition is hidden.</param>
    public async Task HideTransition(float duration = 2)
    {
        // Initialize transition progress at the beginning of the hide transition
        transitionAnimator.profile.invert = true;

        transitionAnimator.Play();

        while (transitionAnimator.progress < 1)
        {
            await Task.Yield();  // Yield to allow other operations between frames
        }
    }

    /// <summary>
    /// Sets up the TransitionAnimator component if not already assigned.
    /// </summary>
    private void Start()
    {
        transitionAnimator ??= GetComponent<TransitionAnimator>();
    }
}
