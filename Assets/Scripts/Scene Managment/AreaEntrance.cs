using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string _transitionName;

    private void Start()
    {
        if (_transitionName == SceneManagment.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = transform.position;
        }
    }
}
