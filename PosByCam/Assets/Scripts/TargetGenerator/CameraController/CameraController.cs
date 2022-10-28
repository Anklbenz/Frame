using UnityEngine;

[System.Serializable]
public class CameraController : MouseMove {
    [SerializeField] private Transform cameraRig;
    [SerializeField] private float zoomAcceleration;

    private Camera _camera;
    private Vector3 _rotateStartPos, _dragStartPos ;
    private bool _isDragging, _isRotating;

    public void Init(Camera cam) => _camera = cam;
    
    protected override void Rotate(){
        Debug.Log(_camera.projectionMatrix);
        if (!_isRotating){
            _rotateStartPos = _camera.ScreenToViewportPoint(mousePosition);
            _isRotating = true;
        }

        var direction = _rotateStartPos - _camera.ScreenToViewportPoint(mousePosition);
        _rotateStartPos = _camera.ScreenToViewportPoint(mousePosition);

        cameraRig.Rotate(Vector3.right, direction.y * 180);
        cameraRig.Rotate(Vector3.up, -direction.x * 180, Space.World);
    }

    protected override void Drag(){
        var ray = _camera.ScreenPointToRay(mousePosition);
        var plane = new Plane(_camera.transform.forward, Vector3.zero);

        if (!plane.Raycast(ray, out var enter)) return;

        if (!_isDragging){
            _dragStartPos = ray.GetPoint(enter);
            _isDragging = true;
        }

        var currentPosition = ray.GetPoint(enter);

        var dragDifference = _dragStartPos - currentPosition;
        cameraRig.transform.position += dragDifference;
    }

    protected override void Zoom(){
        var zoom = Vector3.forward * (zoomAmount * zoomAcceleration);
        _camera.transform.localPosition += zoom;
    }

    protected override void StopDrag() => _isDragging = false;


    protected override void StopRotate() => _isRotating = false;
}
