using UnityEngine;

namespace WFIG
{
    public class TestSimpleObjectPool : MonoBehaviour
    {
        private GameObject testObject;

        private void Start()
        {
            Invoke(nameof(GetRandomTestObject),1);
        }

        private void GetRandomTestObject()
        {
            testObject = SimpleObjectPool.Instance.GetObjectFromPool("Test");
            
            if(testObject is null)
                return;
            
            testObject.transform.position = Vector3.one * Random.Range(-1, 1);
            testObject.transform.localEulerAngles = Vector3.one * Random.Range(-20, 20);
            Invoke(nameof(ReturnPoolObjects),1);
        }

        private void ReturnPoolObjects()
        {
            SimpleObjectPool.Instance.ReturnObjectToPool("Test",testObject);
            Invoke(nameof(GetRandomTestObject),2);
        }
    }
}