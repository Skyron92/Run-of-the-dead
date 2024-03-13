using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerUIManager : MonoBehaviour {

    public void ReturnMenu() {
        SceneManager.LoadScene(0);
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}