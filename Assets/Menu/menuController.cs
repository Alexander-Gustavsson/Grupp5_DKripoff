using UnityEngine;
// using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    //private void Start()
    //{
    //    AudioManager.PlaySound(SoundType.AMBIANCE, 0.6f);
    //}
    public void LoadGame()
    {
        GameObject.Find("AudioManager").GetComponent<Music>().SmoothSound(0.3f, 2f);

        SceneManager.LoadScene(1);
    }
}