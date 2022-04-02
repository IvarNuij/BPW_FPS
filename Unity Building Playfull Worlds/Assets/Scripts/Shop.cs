using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private Shooting ShootingScript;

    public float Coins = 0f;
    public bool ShopActive = false;

    private void Start()
    {
        ShootingScript = FindObjectOfType<Shooting>();
    }

    private void Update()
    {
        if (ShopActive == true)
        {
           if (Input.GetKeyDown("2") && ShootingScript.ShotgunUnlocked == false)
            {
                if (Coins >= 100f)
                {
                    Coins -= 100f;
                    ShootingScript.ShotgunUnlocked = true;
                }
            }

           if (Input.GetKeyDown("3") && ShootingScript.MachinegunUnlocked == false)
            {
                if (Coins >= 300f)
                {
                    Coins -= 300f;
                    ShootingScript.MachinegunUnlocked = true;
                }
            }
        }
    }
}
