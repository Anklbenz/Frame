using UnityEngine;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class EdgeImageGetter {
    [SerializeField] private EdgeSettings edgeSettings;
    [SerializeField] private Shader replaceShader;
    
    private ShaderReplacement _shaderReplacement = new();
    private Screenshot _screenshot = new();
    private Vector2Int _frameSize;

    public void Init(Camera cam,  Vector2Int frameSize){
        _frameSize = frameSize;
        _shaderReplacement.Init(cam, replaceShader);
    }

    public async UniTask<Texture2D> GetImage(MonoBehaviour coroutineRunner){
        _shaderReplacement.ReplaceShader();
        var screen = await _screenshot.Take(coroutineRunner, _frameSize);
        _shaderReplacement.ResetReplaceShader();

        return GetEdges(screen);
    }


    private Texture2D GetEdges(Texture2D texture){
        var mat = OpenCvSharp.Unity.TextureToMat(texture);
        mat = CvFeatures.GetEdges(mat, edgeSettings);
 
        return OpenCvSharp.Unity.MatToTexture(mat);
    }
}
