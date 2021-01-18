using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Player Events Channel")]
public class PlayerEventsChannel: ScriptableObject {

    public UnityAction<Player, AttackType> OnPlayerBlocked;
    public void PlayerBlocked(Player player, AttackType atk) {
        OnPlayerBlocked?.Invoke(player, atk);
    }

    public UnityAction<Player, AttackType> OnPlayerGotHit;
    public void PlayerGotHit(Player player, AttackType atk) {
        OnPlayerGotHit?.Invoke(player, atk);
    }

}