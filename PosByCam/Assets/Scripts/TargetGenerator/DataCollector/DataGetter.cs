using Cysharp.Threading.Tasks;
using OpenCvSharp.Util;
using UnityEngine;

[System.Serializable]
public class DataGetter {
    [SerializeField] private EdgeImageGetter edgeImageGetter;
    [SerializeField] private MonoBehaviour runner;
    [SerializeField] private FinderView finderView;
    public Data data{ get; private set; }

    private RelativeTransformMatrix _relativeTransformMatrix = new();

    private UniTaskCompletionSource<Data> _completionSource;
    private Vector3 pos, pos1;

    public void Init(Camera cam, Transform target, Vector2Int imageFrameSize){
 //---test
        pos = cam.transform.position;
        pos1 = target.transform.position;
//---test
        _relativeTransformMatrix.Init(cam.transform, target);
        edgeImageGetter.Init(cam, imageFrameSize);

        finderView.Init(imageFrameSize);
        finderView.shotButton.onClick.AddListener(Get);
    }

    public async UniTask<Data> Processing(){
        finderView.Show();

        _completionSource = new UniTaskCompletionSource<Data>();
        return await _completionSource.Task;
    }

    public async void Get(){
        finderView.Hide();

        data = new Data
        {
            Texture = await edgeImageGetter.GetImage(runner),
            Position = _relativeTransformMatrix.GetPosition(),
            Rotation = _relativeTransformMatrix.GetRotation().eulerAngles
        };

        Debug.Log("Dist " + Vector3.Distance(Vector3.zero, data.Position));
        _completionSource.TrySetResult(data);
    }
}
