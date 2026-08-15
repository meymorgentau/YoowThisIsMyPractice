using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject менюОкончания;

    void Start()
    {
        менюОкончания.SetActive(false);
    }

    public void ПоказатьМеню()
    {
        менюОкончания.SetActive(true);
        Time.timeScale = 0f;
    }

    public void НачатьЗаново()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Выйти()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}