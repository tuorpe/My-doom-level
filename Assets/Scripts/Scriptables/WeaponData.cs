using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public enum Type
    {
        Pistol,
        Shotgun,
        ChainGun,
        RocketLauncher,
        PlasmaRifle,
        FBG9000,
        Railgun,
        other
    }
    public Type type;
    public float damage;
    public float fireRate;
    public int currentAmmo;
    public int magazineSize;
    public float blastRadius;
    public bool isAutomatic;
    public bool canHurtPlayer;
    
}
