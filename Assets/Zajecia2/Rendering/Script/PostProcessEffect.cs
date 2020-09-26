using UnityEngine;

[ExecuteInEditMode]
public class PostProcessEffect : MonoBehaviour {

    [SerializeField] Material postProcessMat;

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, postProcessMat);
    }
}
