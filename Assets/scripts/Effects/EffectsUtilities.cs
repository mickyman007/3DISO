using UnityEngine;

public static class EffectsUtilities {
    private static string outlineShader = "Outlined/Silhouetted Diffuse";

    public static void Expand(this Transform transform, float mult) {
        transform.localScale *= mult;
    }

    public static void Shrink(this Transform transform, float mult) {
        transform.localScale /= mult;
    }

    public static void ToggleOutLine(this Material material, Material originalMaterial) {
        if(material.shader.name == outlineShader) {
            material.CopyPropertiesFromMaterial(originalMaterial);
            material.shader = originalMaterial.shader;
            return;
        }

        var tex = originalMaterial.mainTexture;
        material.shader = Shader.Find(outlineShader);
        material.SetColor("_Color", new Color(255, 255, 255, 160));
        material.SetColor("_OutlineColor", new Color(255, 255, 0));
        material.SetFloat("Outline", 0.5f);
        material.SetTexture("_MainTex", tex);
    }
}
