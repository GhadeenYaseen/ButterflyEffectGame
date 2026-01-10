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

    private BasicRigidBodyPush _playerPushAbility;

    [Header("Quest1-Speed Settings")]
    [SerializeField] private GameObject boltIcon;
    [Tooltip("This amount will be ADDED to current speed value set up in ThirdPersonController component on player object")]
    [SerializeField] private float speedGainAmount;
    [Tooltip("This amount will be ADDED to current sprint speed value set up in ThirdPersonController component on player object")]
    [SerializeField] private float sprintSpeedGainAmount;

    private void Awake() 
    {
        instance = this;
        _playerPushAbility = player.gameObject.GetComponent<BasicRigidBodyPush>();
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
        boltIcon.SetActive(true);
        player.MoveSpeed += speedGainAmount;
        player.SprintSpeed += sprintSpeedGainAmount;
        player.Input.sprint = true;
        
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
