using UnityEngine;

public class RelativeTransformMatrix {
    private Transform _mainTransform, _targetTransform;

    public void Init(Transform main, Transform target){
        _mainTransform = main;
        _targetTransform = target;
    }

    public Vector3 GetPosition(){
        return _mainTransform.InverseTransformPoint(_targetTransform.position);
    }

    public Quaternion GetRotation(){
        return Quaternion.Inverse(_mainTransform.rotation) * _targetTransform.rotation;
    }
}
