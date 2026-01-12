using UnityEngine;

public class ScenesChangerButton : MonoBehaviour
{
    public void ChangeScene(string sceneName) 
    {
        ScenesMngr.instance.StartLoading(sceneName);
    }
}
