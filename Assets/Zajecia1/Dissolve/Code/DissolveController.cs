using Features.Dissolve;
using UnityEngine;

public class DissolveController : MonoBehaviour {

    void Update() {
        
        if (DissolveSettings.Settings.CutOffEnabled)
            Shader.EnableKeyword("ALPHA_CUTOFF_ENABLED");
        else 
            Shader.DisableKeyword("ALPHA_CUTOFF_ENABLED");
        
        Shader.SetGlobalFloat("_CutOffValue",DissolveSettings.Settings.CutOffValue);
        
    }
}
