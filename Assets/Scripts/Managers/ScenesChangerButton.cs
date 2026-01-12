using UnityEngine;

public class ScenesChangerButton : MonoBehaviour
{
    public void ChangeScene(string sceneName) 
    {
        ScenesMngr.ScenesMngrInstance.StartLoading(sceneName);
    }
}
