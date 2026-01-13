using StarterAssets;
using UnityEngine;
public enum QuestType
{
    SpeedGain,
    SuperJump,
    PushAbility,
    FinalStop
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance {get; private set;}

    [SerializeField] private ThirdPersonController player;
    [SerializeField] private AudioSource questSFX;

    private BasicRigidBodyPush _playerPushAbility;

#region Quest settings
    [Header("Quest1-Speed Settings")]
    [SerializeField] private GameObject boltIcon;
    [Tooltip("This amount will be ADDED to current speed value set up in ThirdPersonController component on player object")]
    [SerializeField] private float speedGainAmount;
    [Tooltip("This amount will be ADDED to current sprint speed value set up in ThirdPersonController component on player object")]
    [SerializeField] private float sprintSpeedGainAmount;

    [Header("Quest2-Jump Settings")]
    [SerializeField] private GameObject jumpIcon;
    [Tooltip("This amount will be ADDED to current jump value set up in ThirdPersonController component on player object")]
    [SerializeField] private float jumpHeightGainAmount;
    [Tooltip("This amount will be SUBTRACTED from current GRAVITY value set up in ThirdPersonController component on player object")]
    [SerializeField] private float fallSpeedGainAmount;

    [Header("Quest3-Push Settings")]
    [SerializeField] private GameObject pushIcon;
    [Tooltip("This amount will be ADDED to current strength value set up in BasicRigidBodyPush component on player object")]
    [SerializeField] private float pushStrengthAmount;
#endregion

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

            case QuestType.PushAbility:
                UpdatePlayerPushAbility();
            break;

            case QuestType.FinalStop:
                FinalDestination();
            break;
            
            default:
                Debug.LogError("wthelly type quest is this shih?");
            break;
        }

        UIManager.instance.UpdateTitles(quest);
        questSFX.Play();
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
        player.JumpHeight += jumpHeightGainAmount;
        player.Gravity -= fallSpeedGainAmount;
        jumpIcon.SetActive(true); 

        Debug.Log("increase jump power and fall speed");
    }

    private void UpdatePlayerPushAbility()
    {
        pushIcon.SetActive(true);
        _playerPushAbility.strength += pushStrengthAmount;
        _playerPushAbility.canPush = true;

        Debug.Log("gain ability to push stuff :0");
    }

    //if player collides with final trigger, stop timer, trigger win sequence
    private void FinalDestination()
    {
        TimeOutManager.instance.StopTimer();
        UIManager.instance.WinScreen();
    }
}
