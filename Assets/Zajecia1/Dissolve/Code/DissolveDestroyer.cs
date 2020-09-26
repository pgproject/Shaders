using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Dissolve {
	
	public class DissolveDestroyer : MonoBehaviour {
		
		public Action OnDissolveDone;
		
		CreatedMaterials createdMaterials = new CreatedMaterials();
		
		int numberOfRenderers;
		
		static readonly int DissolveMapProperty = Shader.PropertyToID("_DissolveMap");
		static readonly int DissolveEffectProperty = Shader.PropertyToID("_DissolveEffect");
		static readonly int DissolveEffectSizeProperty = Shader.PropertyToID("_DissolveEffectSize");
		static readonly int DissolveAmountProperty = Shader.PropertyToID("_DissolveAmount");

		public void RunDestroyer(List<Renderer> renderers, float time = 2f) {
			numberOfRenderers = renderers.Count;
			if (numberOfRenderers > 0) {
				AssignProperShaderToRenderers(renderers);
				StartCoroutine(DissolveCoroutine(time));	
			}
			else 
				OnDissolveDone?.Invoke();
		}

		void AssignProperShaderToRenderers(List<Renderer> renderers) {
			
			for (var i = 0; i < numberOfRenderers; i++) {
				var materials = renderers[i].materials;
				for (var j = 0; j < materials.Length; j++) {
					
					var rendererMaterial = materials[j];
					createdMaterials.AddNewMaterial(rendererMaterial);

					rendererMaterial.shader = DissolveSettings.Settings.DissolveShader;
					rendererMaterial.SetTexture(DissolveMapProperty, DissolveSettings.Settings.SimpleDissolveMap);
					rendererMaterial.SetTexture(DissolveEffectProperty, DissolveSettings.Settings.DissolveEffect_RedFire);
					rendererMaterial.SetFloat(DissolveEffectSizeProperty, DissolveSettings.Settings.EffectAmount);
				}
			}
		}

		IEnumerator DissolveCoroutine(float time = 2f) {
			var timeLeft = time;
			
			while (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
				var dissolveValue = (time - timeLeft) / time;
				createdMaterials.SetDissolveAmount(dissolveValue);
				yield return null;
			}
			createdMaterials.SetDissolveAmount(1f);
			yield return null;
			createdMaterials.DestroyAllCreatedMaterials();
			OnDissolveDone?.Invoke();
		}

		class CreatedMaterials {
			
			int numberOfChangedMaterials;
			List<Material> allChangedMaterials = new List<Material>();

			public void AddNewMaterial(Material createdMaterial) {
				allChangedMaterials.Add(createdMaterial);
				numberOfChangedMaterials++;
			}

			public void SetDissolveAmount(float dissolveAmount) {
				for (var i = 0; i < numberOfChangedMaterials; i++) 
					allChangedMaterials[i].SetFloat(DissolveAmountProperty, dissolveAmount);
			}

			public void DestroyAllCreatedMaterials() {
				for (var i = 0; i < numberOfChangedMaterials; i++) 
					Destroy(allChangedMaterials[i]);
			}
		}
	}
}
