using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {get; private set;}

    [Header("Quests Text")]
    [SerializeField] private float titleFadeDuration;
    [SerializeField] private TextMeshProUGUI speedTitle;
    [SerializeField] private TextMeshProUGUI jumpTitle;
    [SerializeField] private TextMeshProUGUI pushTitle;

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        speedTitle.gameObject.SetActive(false);
        jumpTitle.gameObject.SetActive(false);
        pushTitle.gameObject.SetActive(false);
    }

    public void UpdateTitles(QuestType quest)
    {
        switch (quest)
        {
            case QuestType.SpeedGain:
                ActivateQuestTitle(speedTitle);
            break;

            case QuestType.SuperJump:
                ActivateQuestTitle(jumpTitle);
            break;

            case QuestType.PushAbility:
                ActivateQuestTitle(pushTitle);
            break;
            
            default:
                Debug.LogError("wthelly type quest is this fih?");
            break;
        }
    }

    private void ActivateQuestTitle(TextMeshProUGUI title)
    {
        title.gameObject.SetActive(true);
        title.DOFade(1f, titleFadeDuration);
    }
}
