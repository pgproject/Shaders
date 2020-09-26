using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushController : MonoBehaviour {

    [SerializeField] Camera mainCamera;
    [SerializeField] Camera brushCamera;
    
    static readonly int CustomPainting = Shader.PropertyToID("_CustomPainting");

    void Awake() {
        Graphics.Blit(null, brushCamera.targetTexture);
        Shader.SetGlobalTexture(CustomPainting, brushCamera.targetTexture);
    }
    
    void Update() {
        var mouse = Input.mousePosition;
        var castPoint = mainCamera.ScreenPointToRay(mouse);
        if (Physics.Raycast(castPoint, out var hit))
            transform.position = hit.point;
    }
}
