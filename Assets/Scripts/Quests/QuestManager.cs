using StarterAssets;
using UnityEngine;
public enum QuestType
{
    SpeedGain,
    SuperJump,
    ChangePlayerSkin,
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance {get; private set;}

    [SerializeField] private ThirdPersonController player;

    private void Awake() 
    {
        instance = this;
    }

    public void GrantQuest(QuestType quest)
    {
        switch (quest)
        {
            case QuestType.SpeedGain:
                UpdatePlayerSpeed();
            break;

            case QuestType.SuperJump:
                UpdatePlayerJump();
            break;

            case QuestType.ChangePlayerSkin:
                UpdatePlayerGraphics();
            break;
            
            default:
                Debug.LogError("wthelly type quest is this shih?");
            break;
        }

        UIManager.instance.UpdateTitles(quest);
    }

    private void UpdatePlayerSpeed()
    {
        Debug.Log("unlock shift for speed + increase regular speed");
    }

    private void UpdatePlayerJump()
    {
        Debug.Log("increase jump power and fall speed");
    }

    private void UpdatePlayerGraphics()
    {
        Debug.Log("unlock cool meshes ontop of regular mesh :0");
    }
}
