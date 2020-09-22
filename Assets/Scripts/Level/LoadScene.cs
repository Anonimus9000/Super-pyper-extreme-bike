using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;
    
    public void Load()
    {
        Application.LoadLevel(_sceneToLoad);
    }
}
