using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CenterFinder : MonoBehaviour {
    [SerializeField] private GameObject test, prefab;
  
    private readonly List< Vector3> _allVertices = new();
    private List<Vector3> _cameraRelativeVertices = new();
    private Vector3 _projectionCenterRalative, _cameraRelative;
    private Quaternion _rotation;
    private Vector3 _rotationVector3;
    
    private MeshFilter[] _allChildrenMeshFilters;
    private Camera _camera;
    private Bounds _bounds ;
    private Matrix4x4 transformMatrix ;
    private Vector3 pos;
    
    
    private Vector3 pos1 = new Vector3(0.02f, -1.63f, 0.73f);
    private Quaternion rot1 = new Quaternion(0, -0.70711f, 0.70711f, 0);
   
    private void Awake(){
        _camera = Camera.main;
        _allChildrenMeshFilters = GetAllMeshFilters(test);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){

            _allVertices.Clear();
            _cameraRelativeVertices.Clear();
            foreach (var meshFilter in _allChildrenMeshFilters)
                _allVertices.AddRange(meshFilter.GetHashVerticesWorldPosition());

            foreach (var vertex in _allVertices)
                _cameraRelativeVertices.Add(_camera.transform.InverseTransformPoint(vertex));

            _bounds = GetProjectionBounds();

          //  var point = _camera.transform.TransformPoint(_bounds.center.x, _bounds.center.y, _bounds.max.z);
            //   transformMatrix = Matrix4x4.TRS(point, _camera.transform.rotation, Vector3.one);
         //   transformMatrix = Matrix4x4.TRS(_camera.transform.position, _camera.transform.rotation, Vector3.one);
            pos = _camera.transform.InverseTransformPoint(test.transform.position);
            _rotation = Quaternion.Inverse(_camera.transform.rotation) * test.transform.rotation;
        
            
          //   _projectionCenterRalative = transformMatrix.inverse.MultiplyPoint3x4(test.transform.position);
            
            Debug.Log(pos +"\n" + _rotation.eulerAngles);
            
            //_projectionCenterRalative = transformMatrix.inverse.MultiplyPoint3x4(test.transform.position);
        //    _rotationVector3 = transformMatrix.inverse.MultiplyVector((test.transform.rotation).eulerAngles);

            //   _cameraRelative = _camera.transform.InverseTransformPoint(test.transform.position);

            // transformMatrix.
        }

        if (Input.GetKeyDown(KeyCode.Backspace)){
            Destroy(test.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter)){
            var rot  = _camera.transform.rotation * _rotation;
            
            var o =Instantiate(prefab, _camera.transform.TransformPoint(pos), rot);
            
          
            //obj.transform.localToWorldMatrix.rotation
            //Instantiate(prefab, _camera.transform.TransformPoint(_cameraRelative), Quaternion.Inverse(_rotation));
            //    transformMatrix.MultiplyPoint3x4
        }

        if (Input.GetKeyDown(KeyCode.A)){
            var position = _camera.transform.TransformPoint(pos1);
            var rotation = _camera.transform.rotation * rot1;
            var o = Instantiate(prefab, position, rotation);
        }
    }

    private Bounds GetProjectionBounds(){
        float minX, maxX, minY, maxY, maxZ, minZ;

        minX = maxX = _cameraRelativeVertices[0].x;
        minY = maxY = _cameraRelativeVertices[0].y;
        minZ = maxZ = _cameraRelativeVertices[0].z;

        foreach (var vertex in _cameraRelativeVertices){
            minX = minX > vertex.x ? vertex.x : minX;
            maxX = maxX < vertex.x ? vertex.x : maxX;

            minY = minY > vertex.y ? vertex.y : minY;
            maxY = maxY < vertex.y ? vertex.y : maxY;

            minZ = minZ > vertex.z ? vertex.z : minZ;
            maxZ = maxZ < vertex.z ? vertex.z : maxZ;
        }

        var center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (minZ + maxZ) / 2);
        var size = new Vector3(Mathf.Abs(maxX - minX), Mathf.Abs(maxY - minY), Mathf.Abs(maxZ - minZ));

        return new Bounds(center, size);
    }

    private MeshFilter[] GetAllMeshFilters(GameObject go){
        return go.GetComponentsInChildren<MeshFilter>();
    }

    private void OnDrawGizmos(){
        if(_camera == null) return;
        Gizmos.color = Color.magenta;
        Gizmos.matrix = _camera.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(_bounds.center.x, _bounds.center.y, _bounds.max.z), new Vector3(0.05f,0.05f));
    }
}
