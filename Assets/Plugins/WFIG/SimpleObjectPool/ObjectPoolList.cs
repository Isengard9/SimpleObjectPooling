using System.Collections.Generic;
using UnityEngine;

namespace WFIG
{
    [CreateAssetMenu(fileName = "ObjectPoolList", menuName = "WFIG/Object Pool Item List", order = 1)]
    public class ObjectPoolList: ScriptableObject
    {
        public List<ObjectPoolItem> ItemList = new List<ObjectPoolItem>();
    }
}