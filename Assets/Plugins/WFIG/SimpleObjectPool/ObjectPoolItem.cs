using UnityEngine;

namespace WFIG
{
    [CreateAssetMenu(fileName = "ObjectPoolItem", menuName = "WFIG/Object Pool Item", order = 2)]
    public class ObjectPoolItem : ScriptableObject
    {
        public string objectName;
        public GameObject prefab;
        public int poolSize = 10;
    }
    
}
