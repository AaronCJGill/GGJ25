using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode,ImageEffectAllowedInSceneView]

public class ImageEffectBasic : MonoBehaviour
{
    public Material effectMaterial;
    public static bool EffectEnabled = true;


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (EffectEnabled)
            Graphics.Blit(source, destination, effectMaterial);
    }
}

