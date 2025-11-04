using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public enum Type
        {
            HealthPack,
            Armor,
            Ammo,
            Weapon,
            Keycard,
            Other
        }
        // The type of the item
        public Type itemType;
        // Value associated with the item (e.g., health amount, ammo count)
        public int value;
        public WeaponData.Type ammoType;
    }
}
