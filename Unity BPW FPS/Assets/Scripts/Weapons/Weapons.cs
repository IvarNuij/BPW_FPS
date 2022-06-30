using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    public GameObject weapon;

    public int range;
    public int damage;

    //Ammo
    public int currentAmmo;
    public int magazineSize;
    public int maxAmmo;
    public int currentMaxAmmo;

    //bullet
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private Transform bulletSpawnLocation;

    //Cooldown
    public float shootCooldown;
    public float currentShootCooldown; 
    public float reloadCooldown;
    public float currentReloadCooldown; 

    //References
    private CharacterController player;
    private Camera playerCamera;
    private Enemy enemy;

    private void Start()
    {
        player = FindObjectOfType<CharacterController>();
        playerCamera = player.transform.GetChild(0).GetComponent<Camera>();
        bulletSpawnLocation = FindObjectOfType<FindBulletSpawnLocation>().transform;
    }

    public virtual void Update()
    {
        //Inputs
        if (Input.GetButtonDown("Fire1") && currentAmmo >= 1 && currentShootCooldown <= 0 && currentReloadCooldown <= 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown("r") && currentReloadCooldown <= 0 && currentMaxAmmo >= magazineSize)
        {
            Reload();
        }

        currentShootCooldown = Cooldowns(currentShootCooldown);
        currentReloadCooldown = Cooldowns(currentReloadCooldown);
    }

    public virtual void Shoot()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit Hit, range))
        {
            enemy = Hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        //Instantiate Bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(weapon.transform.forward * (bulletSpeed * Time.deltaTime));
        Destroy(bullet.gameObject, 1);

        currentShootCooldown = shootCooldown;

        currentAmmo -= 1;
    }

    public virtual void Reload()
    {
        //Calc Bullets
        int bulletsToReload = magazineSize - currentAmmo;
        currentAmmo += bulletsToReload;
        currentMaxAmmo -= bulletsToReload;

        currentReloadCooldown = reloadCooldown;
    }

    public float Cooldowns(float currentCooldown)
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        return currentCooldown;
    }
}