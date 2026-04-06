using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
