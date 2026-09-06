using UnityEngine;

public class ГлавноеМеню : MonoBehaviour
{
    public GameObject меню;

    private void Start()
    {
        меню.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Играть()
    {
        DinoController.автоматическийРежим = false;

        меню.SetActive(false);
        Time.timeScale = 1f;
    }

    public void АвтоматическийРежим()
    {
        DinoController.автоматическийРежим = true;

        меню.SetActive(false);
        Time.timeScale = 1f;
    }
}       