using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private string _sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<PlayerController>()) return;
        
        SceneManagment.Instance.SetTransitionName(_sceneTransitionName);
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return StartCoroutine(UIFade.Instance.FadeToBlack());
        SceneManager.LoadScene(_sceneToLoad);
    }
}
