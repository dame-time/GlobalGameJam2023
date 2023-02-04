using DG.Tweening;
using UnityEngine;

public class LevelTransitionEffect : PostProcessCamera
{
    [Range(0.0f, 1.0f)] [SerializeField] private float progressTransition;

    public static LevelTransitionEffect instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartTransition()
    {
        isEnable = true;

        float time = 1.0f;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => progressTransition, x => progressTransition = x, 1, time))
                .Append(DOTween.To(() => progressTransition, x => progressTransition = x, 0, time))
                .AppendCallback(() => { isEnable = false; });
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
