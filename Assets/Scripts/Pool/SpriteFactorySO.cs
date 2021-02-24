using UnityEngine;
using Bolza.Factory;

[CreateAssetMenu(fileName = "NewSpriteFactory", menuName = "Factory/Sprite Factory")]
public class SpriteFactorySO : FactorySO<GameObject> {
    [SerializeField] private GameObject prefab;
    public override GameObject Create() {
        return Instantiate(prefab);
    }
}
