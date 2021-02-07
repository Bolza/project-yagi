using UnityEngine.Events;
using UnityEngine;

public abstract class Interaction : MonoBehaviour {
    public string interactionName;
    public string description;
    public bool playerOnly;
    public bool isAvailable { get; private set; }
    protected float duration;
    private float startTime;

    public UnityAction<GameObject> OnInteract;

    public virtual void Interact(GameObject other) {
        if (!isAvailable) return;
        OnInteract?.Invoke(other);
        isAvailable = false;
        startTime = Time.time;
    }

    public virtual void Update() {
        if (!isAvailable && Time.time >= startTime + duration) isAvailable = true;
    }
}