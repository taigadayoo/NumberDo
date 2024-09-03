using System;
using System.IO;
using Live2D.Cubism.Framework.Json;
using UnityEngine;
/// <summary>
/// Initialize model.
/// </summary>
public class InitModel : MonoBehaviour
{
    void Start()
    {
        //Load model.
        var path = Application.streamingAssetsPath + "/koharu.model3.json";
        var model3Json = CubismModel3Json.LoadAtPath(path, BuiltinLoadAssetAtPath);
        var model = model3Json.ToModel();
    }
    /// <summary>
    /// Load asset.
    /// </summary>
    /// <param name="assetType">Asset type.</param>
    /// <param name="absolutePath">Path to asset.</param>
    /// <returns>The asset on succes; <see langword="null"> otherwise.</returns>
    public static object BuiltinLoadAssetAtPath(Type assetType, string absolutePath)
    {
        if (assetType == typeof(byte[]))
        {
            return File.ReadAllBytes(absolutePath);
        }
        else if (assetType == typeof(string))
        {
            return File.ReadAllText(absolutePath);
        }
        else if (assetType == typeof(Texture2D))
        {
            var texture = new Texture2D(1, 1);
            texture.LoadImage(File.ReadAllBytes(absolutePath));
            return texture;
        }
        throw new NotSupportedException();
    }
}