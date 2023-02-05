using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LevelTransitionEffect : PostProcessCamera
{
    [Range(0.0f, 1.0f)] [SerializeField] private float progressTransition;

    public static LevelTransitionEffect instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public IEnumerator StartTransition()
    {
        isEnable = true;

        float time = 0.75f;

        Sequence sequence = DOTween.Sequence();
        yield return sequence.Append(DOTween.To(() => progressTransition, x => progressTransition = x, 1, time))
                .Append(DOTween.To(() => progressTransition, x => progressTransition = x, 0, time))
                .AppendCallback(() => { isEnable = false; }).WaitForCompletion();
    }

    public IEnumerator StartTransition(TweenCallback callback, bool destroyAtEnd = false)
    {
        isEnable = true;

        float time = 0.75f;

        Sequence sequence = DOTween.Sequence();
        yield return sequence.Append(DOTween.To(() => progressTransition, x => progressTransition = x, 1, time))
                .AppendCallback(callback)
                .Append(DOTween.To(() => progressTransition, x => progressTransition = x, 0, time))

                .WaitForCompletion();
    }

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (isEnable)
        {
            _material.SetFloat("_Progress", progressTransition);
        }

        base.OnRenderImage(source, destination);
    }
}
