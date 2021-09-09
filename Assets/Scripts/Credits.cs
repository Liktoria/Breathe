using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void LoadNextScene(string scenename)
    {
        SceneManager.LoadScene("Credits");
    }
}
