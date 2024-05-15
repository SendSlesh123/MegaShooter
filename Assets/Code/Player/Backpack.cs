using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Backpack : MonoBehaviour
{
    public GameObject[] weapons; // Массив объектов оружия
    private int currentWeaponIndex = 0; // Индекс текущего оружия

    void Start()
    {
        // Деактивируем все оружия кроме первого
        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    void Update()
    {
        // Проверяем нажатие клавиш для смены оружия
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
        {
            SwitchWeapon(1);
        }
        
    }

    void SwitchWeapon(int newIndex)
    {
        // Деактивируем текущее оружие
        weapons[currentWeaponIndex].SetActive(false);

        // Устанавливаем новое текущее оружие
        currentWeaponIndex = newIndex;

        // Активируем новое текущее оружие
        weapons[currentWeaponIndex].SetActive(true);
    }

}
