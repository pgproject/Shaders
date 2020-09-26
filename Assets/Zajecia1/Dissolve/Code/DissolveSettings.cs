
namespace Features.Dissolve {
    
    using System;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Dissolve Settings")]
    public class DissolveSettings : ScriptableObject {

    [Serializable]
        public class Dissolve {

            public Shader DissolveShader => _DissolveShader;
            public Texture SimpleDissolveMap => _SimpleDissolveMap;
            public Texture DissolveEffect_RedFire => _DissolveEffect_RedFire;
            
            public bool CutOffEnabled => _CutOffEnabled;
            public float CutOffValue => _CutOffValue;
            public float EffectAmount => _EffectAmount;

            [Header("Global")]
            [SerializeField] Shader _DissolveShader;
            [SerializeField] Texture _SimpleDissolveMap;
            [SerializeField] Texture _DissolveEffect_RedFire;
            
            [SerializeField] bool _CutOffEnabled;
            [SerializeField] [Range(0, 1)] float _CutOffValue;
            [SerializeField] [Range(0, 1)] float _EffectAmount = 0.025f;
        }

        public static Dissolve Settings {
            get {
                if (_GlobalSettings != null) return _GlobalSettings;
                var resources = Resources.LoadAll<DissolveSettings>(string.Empty);
                _GlobalSettings = resources[0]._Settings;
                return _GlobalSettings;
            }
        }

        [SerializeField] Dissolve _Settings;
        static Dissolve _GlobalSettings;
    }
}