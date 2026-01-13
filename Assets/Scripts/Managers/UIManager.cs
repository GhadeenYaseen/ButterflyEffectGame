using DG.Tweening;
using StarterAssets;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {get; private set;}

    [SerializeField] private ThirdPersonController player;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private AudioSource uiClickSFX;

    [Header("Quests Text")]
    [SerializeField] private float titleFadeDuration;
    [SerializeField] private TextMeshProUGUI speedTitle;
    [SerializeField] private TextMeshProUGUI jumpTitle;
    [SerializeField] private TextMeshProUGUI pushTitle;

    [Header("Screens")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameOverAltScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;

    private void Awake() 
    {
        Time.timeScale = 1;
        instance = this;
    }

    private void Start() 
    {
        speedTitle.gameObject.SetActive(false);
        jumpTitle.gameObject.SetActive(false);
        pushTitle.gameObject.SetActive(false);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            ResumeGame();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            ScenesMngr.instance.StartLoading("Main Menu");
        }
        
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        TurnOffPlayer();
        pauseScreen.SetActive(true);
        uiClickSFX.Play();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        TurnOnPlayer();
        pauseScreen.SetActive(false);
        uiClickSFX.Play();
    }
    
    public void GameOver()
    {
        Time.timeScale = 0;
        TurnOffPlayer();
        gameOverScreen.SetActive(true);
    }

    public void GameOverAltEnding()
    {
        Time.timeScale = 0;
        TurnOffPlayer();
        gameOverAltScreen.SetActive(true);
    }

    public void WinScreen()
    {
        Time.timeScale = 0;
        TurnOffPlayer();
        winScreen.SetActive(true);
    }

    private void TurnOffPlayer()
    {
        player.enabled = false;
        playerAnim.enabled = false;
    }

    private void TurnOnPlayer()
    {
        player.enabled = true;
        playerAnim.enabled = true;
    }
}
