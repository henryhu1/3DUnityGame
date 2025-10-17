using UnityEditor;
using UnityEngine;

public class RelinkURPTextures
{
    [MenuItem("Tools/Relink URP Textures")]
    static void Relink()
    {
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        int updatedCount = 0;
        int updatedNormalCount = 0;
        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log(path);
            var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null || !mat.shader.name.Contains("Universal Render Pipeline/Lit")) continue;

            string folder = System.IO.Path.GetDirectoryName(path);
            string baseName = System.IO.Path.GetFileNameWithoutExtension(path);

            Texture2D albedo = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Mansion/Assets/Textures/{baseName}.png");
            // Debug.Log($"Textures/{baseName}.png - {albedo}");

            if (albedo)
            {
                mat.SetTexture("_BaseMap", albedo);
                updatedCount++;
            }

            Texture2D normal = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Mansion/Assets/Textures{baseName}-nrm.png");
            if (normal)
            {
                mat.SetTexture("_BumpMap", normal);
                mat.EnableKeyword("_NORMALMAP");
                updatedNormalCount++;
            }

            EditorUtility.SetDirty(mat);
        }
        AssetDatabase.SaveAssets();
        Debug.Log($"Re-linked URP textures where possible. {updatedCount} {updatedNormalCount}");
    }
}
