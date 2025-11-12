using UnityEngine;

[CreateAssetMenu(menuName = "PP3/Configs/Weapon Config")]
public class WeaponConfigSO : ScriptableObject
{
    public float fireRate = 6f; // shots per second
    public float damage = 20f;
    public float range = 100f;
    public int maxAmmo = 30;
    public GameObject projectilePrefab; // optional
    public AudioClip fireSfx;
    public GameObject muzzleFlashVfx;
}