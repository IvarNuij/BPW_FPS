using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private CharacterControlScript Player;
    private Shooting ShootingScript;

    //JumpCoolDown
    public Slider JumpCooldownSlider;

    //Health
    public Slider HealthSlider;

    //Ammo
    public Text CurrentAmmo;
    public Text TotalAmmo;

    
    //--------------------------------------------//

    void Start()
    {
        Player = FindObjectOfType<CharacterControlScript>();
        ShootingScript = FindObjectOfType<Shooting>();
    }

    void Update()
    {
        JumpCoolDown();
        Health();
        CalcAmmo();
    }

    //----------------------------------------------------//

    void JumpCoolDown()
    {
        JumpCooldownSlider.value = Player.JumpCooldown;
    }

    void Health()
    {
        HealthSlider.value = Player.PlayerHealth;
    }

    void CalcAmmo()
    {
        if (ShootingScript.EquipedWeapon == "Pistol")
        {
            CurrentAmmo.text = ShootingScript.PistolAmmo.ToString() + "/6";
            TotalAmmo.text = ShootingScript.PistolTotalAmmo.ToString();
        }

        if (ShootingScript.EquipedWeapon == "Shotgun")
        {
            CurrentAmmo.text = ShootingScript.ShotgunAmmo.ToString() + "/2";
            TotalAmmo.text = ShootingScript.ShotgunTotalAmmo.ToString();
        }
    }
}