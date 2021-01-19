using UnityEngine;

/// <summary>
/// This class goes on a trigger which, when entered, sends the player to another Location
/// </summary>

public class LocationExitTrigger: MonoBehaviour {
    [Header("Loading settings")]
    [SerializeField] private GameSceneSO[] locationsToLoad = default;
    [SerializeField] private bool showLoadScreen = default;

    [Header("Broadcasting on")]
    [SerializeField] private SceneManagementEventsChannel eventsChannel = default;

    [SerializeField] private LayerMask layerMask = default;

    private void Update() {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1f, layerMask);
        if (collider && collider.CompareTag("Player")) {
            onPlayerEnter(collider);
        }
    }

    private void onPlayerEnter(Collider2D other) {
        Debug.Log("OnTriggerEnter " + other);
        eventsChannel.RequestLoading(locationsToLoad, showLoadScreen);
    }


    //private void UpdatePathTaken()
    //{
    //	if (_pathTaken != null)
    //		_pathTaken.Path = _exitPath;
    //}
}
