using UnityEngine;

public class EntryPoint : MonoBehaviour {
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector2Int frameSize;

    [Space]
    [SerializeField] private CameraController cameraController;

    [SerializeField] private ProcessController processController;

    [Space]
    [SerializeField] private DataGetter dataGetter;
    [Space]
    [SerializeField] private DataSender dataSender;

    private void Awake(){
        dataGetter.Init(cameraMain, targetTransform, frameSize);
        dataSender.Init();
     
        
        cameraController.Init(cameraMain);
        processController.Init(dataGetter, dataSender);
    }

    private void Update(){
        cameraController.Update();
    }
}
