using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Backpack : MonoBehaviour
{
    public GameObject[] weapons; // ������ �������� ������
    private int currentWeaponIndex = 0; // ������ �������� ������

    void Start()
    {
        // ������������ ��� ������ ����� �������
        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    void Update()
    {
        // ��������� ������� ������ ��� ����� ������
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
        // ������������ ������� ������
        weapons[currentWeaponIndex].SetActive(false);

        // ������������� ����� ������� ������
        currentWeaponIndex = newIndex;

        // ���������� ����� ������� ������
        weapons[currentWeaponIndex].SetActive(true);
    }

}
