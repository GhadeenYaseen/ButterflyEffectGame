using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class QuestTriggerer : MonoBehaviour
{
    [SerializeField] private QuestType questType;
    [SerializeField] private GameObject questVFX;
    
    [Header("Disable VFX Settings")]
    [Tooltip("Values below -25 hide object under main ground")]
    [SerializeField] private float targetYLevel;
    [SerializeField] private float duration;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            QuestManager.instance.GrantQuest(questType);
            questVFX.transform.DOMoveY(targetYLevel, duration).SetEase(Ease.InOutSine).
                    OnComplete(()=> questVFX.SetActive(false));
        }
    }
}
