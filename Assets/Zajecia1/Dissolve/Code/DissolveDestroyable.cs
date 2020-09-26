using System.Collections.Generic;
using UnityEngine;

namespace Features.Dissolve {

    public class DissolveDestroyable : MonoBehaviour {

        [SerializeField] List<Renderer> renderersToBurn;

        DissolveDestroyer dissolveDestroyer;
        
        void OnMouseUp() {
            TryToDestroy();
        }

        void TryToDestroy() {
            if (dissolveDestroyer != null) return;
            dissolveDestroyer = gameObject.AddComponent<DissolveDestroyer>();
            dissolveDestroyer.RunDestroyer(renderersToBurn);
            dissolveDestroyer.OnDissolveDone += DissolveDoneHandler;
        }

        void DissolveDoneHandler() {
            Destroy(gameObject);
        }
    }
}
