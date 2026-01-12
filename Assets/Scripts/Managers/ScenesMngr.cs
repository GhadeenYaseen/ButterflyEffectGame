using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesMngr : MonoBehaviour
{
    [HideInInspector] public static ScenesMngr instance {get; private set;}
    
    [SerializeField] public GameObject LoadingScreen;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartLoading(string sceneName) 
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string scenmeName) 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenmeName);
        
        LoadingScreen.SetActive(true);

        while (! operation.isDone) 
        {
            yield return null;
        }

        LoadingScreen.SetActive(false);
    }
}
