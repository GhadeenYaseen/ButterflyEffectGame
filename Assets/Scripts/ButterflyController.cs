using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ButterflyController : MonoBehaviour
{
    [SerializeField] private string flyingIdleAnimationName = "Butterfly_Flying_Idle";

    [Header("Animation Settings")]
    [SerializeField] private float idleAmplitude;
    [SerializeField] private float idleDuration;
    [SerializeField] private Ease idleEase;
    [SerializeField] private LoopType idleLoopType;

    [Header("Exploration Settings")]
    [SerializeField] private List<Transform> destinationsList;

    private Transform _butterflyTransform;
    private Animator _animator;

    private void Awake() 
    {
        _butterflyTransform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayFlyingIdleAnimation();
        FlyToTarget();
    }
    
    private void OnDisable()
    {
        StopFlyingIdleAnimation();
    }

    public void PlayFlyingIdleAnimation()
    {
        _animator.Play(flyingIdleAnimationName);
        _butterflyTransform.DOMoveY(idleAmplitude, idleDuration).SetLoops(-1, idleLoopType).SetEase(idleEase);

        Debug.Log("Butterfly is flying!");
    }

    public void StopFlyingIdleAnimation()
    {
        _butterflyTransform.DOKill();
    }

    // fly to all destinations in the list sequentially
    public void FlyToTarget()
    {
        if (destinationsList == null || destinationsList.Count == 0)
            return;

        Sequence flySequence = DOTween.Sequence();

        for (int i = 0; i < destinationsList.Count; i++)
        {
            int index = i;

            flySequence.Append(_butterflyTransform.DOMove(destinationsList[index].position, 13f).SetEase(Ease.InOutSine)
                .OnUpdate(() =>
                {
                    Vector3 dir = destinationsList[index].position - _butterflyTransform.position;

                    if (dir != Vector3.zero)
                    {
                        Quaternion rot = Quaternion.LookRotation(dir);

                        _butterflyTransform.rotation = Quaternion.Slerp(_butterflyTransform.rotation, rot, Time.deltaTime * 5f);

                    }
                }
                ));

            flySequence.AppendCallback(() =>
            {
                Debug.Log("Butterfly reached " + destinationsList[index].gameObject.name);
            });
        }

        flySequence.OnComplete(() =>
        {
            Debug.Log("Butterfly reached final destination");
            TimeOutManager.instance.StartCountDown();
        });
    }
}
