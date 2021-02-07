using UnityEngine;

/// <summary>
/// This class goes on a trigger which, when entered, sends the player to another Location
/// </summary>

public class LocationExitTrigger : Interaction {
    [Header("Loading settings")]
    [SerializeField] private SceneManagementEventsChannel eventsChannel = default;
    [SerializeField] private GameSceneSO[] locationsToLoad = default;
    [SerializeField] private bool showLoadScreen = default;
    [SerializeField] public PathSO path;

    public LocationExitTrigger() {
        interactionName = "Location Move";
        duration = 1;
    }

    public override void Interact(GameObject other) {
        if (!isAvailable) return;
        base.Interact(other);
        eventsChannel.RequestLoading(locationsToLoad, path, showLoadScreen);
    }
}
