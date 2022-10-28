using UnityEngine;
public class ShaderReplacement {
    private Shader _replaceShader;
    private Camera _camera;

    public void Init(Camera cam, Shader replaceShader){
        _camera = cam;
        _replaceShader = replaceShader;
    }

    public void ReplaceShader(){
        _camera.SetReplacementShader(_replaceShader,"");
    }

    public void ResetReplaceShader(){
        _camera.ResetReplacementShader();
    }
}
