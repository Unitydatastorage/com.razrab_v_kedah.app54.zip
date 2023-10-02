using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("game");
    }
}
