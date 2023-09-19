using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void Move(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
