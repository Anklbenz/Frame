using Cysharp.Threading.Tasks;
using UnityEngine;

public class Screenshot {
  
    public async UniTask<Texture2D> Take(MonoBehaviour coroutineRunner, Vector2Int frameSize){
        var screenShot = new Texture2D(frameSize.x, frameSize.y, TextureFormat.RGB24, false);

        await UniTask.WaitForEndOfFrame(coroutineRunner);

        screenShot.ReadPixels(GetCenterRect(frameSize), 0, 0);
        screenShot.Apply();
        return screenShot;
    }

    private Rect GetCenterRect(Vector2Int frameSize){
        var topX = Screen.width / 2 - frameSize.x / 2;
        var topY = Screen.height / 2 - frameSize.y / 2;
        return new Rect(topX, topY, frameSize.x, frameSize.y);
    }
}
