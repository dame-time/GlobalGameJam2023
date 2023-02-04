using UnityEngine;

public class PostProcessCamera : MonoBehaviour
{
    [SerializeField] protected Material _material;

    [SerializeField] protected bool isEnable;

    protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (isEnable)
        {
            Graphics.Blit(source, destination, _material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
