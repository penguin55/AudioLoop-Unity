using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLoopCallbacks : MonoBehaviour
{
    public delegate void ActiveCallbacks();
    private ActiveCallbacks activeCallbacks;

    private void Update()
    {
        activeCallbacks?.Invoke();
    }

    /// <summary>
    /// Set the CurrentCallbacks to make the Audioloop always looping
    /// </summary>
    /// <param name="_activeCallbacks">The callback method to call every single frame</param>
    public void SetCallbacks(ActiveCallbacks _activeCallbacks)
    {
        activeCallbacks = _activeCallbacks;
    }

    public void Unsubscribe(ActiveCallbacks _activeCallbacks)
    {
        if (_activeCallbacks == activeCallbacks) activeCallbacks = null;
    }
}
