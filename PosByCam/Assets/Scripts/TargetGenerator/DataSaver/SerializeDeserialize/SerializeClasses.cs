using UnityEngine;

namespace ClassesForJson {

    [System.Serializable]
    public class ServerData {
        public int id;
        public int asset_id;
        public string frame;
        public Vector3 origin;
        public Vector3 rotation;
    }

    [System.Serializable]
    public class Id {
        public int id;
    }
}