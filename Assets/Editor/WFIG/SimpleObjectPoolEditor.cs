using System;
using UnityEditor;
using UnityEngine;

namespace WFIG
{
    [CustomEditor(typeof(SimpleObjectPool))]
    public class SimpleObjectPoolEditor : Editor
    {
        private string newObjectName;
        private GameObject newObjectPrefab;
        private int newObjectPoolSize;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);
            

            GUILayout.Label("Add New Object To Pool:", EditorStyles.boldLabel);

            newObjectName = EditorGUILayout.TextField("Object Name", newObjectName);
            newObjectPrefab =
                (GameObject)EditorGUILayout.ObjectField("Prefab", newObjectPrefab, typeof(GameObject), false);
            newObjectPoolSize = EditorGUILayout.IntField("Pool Size", newObjectPoolSize);

            if (GUILayout.Button("Add"))
            {
                if (newObjectPrefab != null)
                {
                    AddNewObjectToPool();
                    newObjectName = "";
                    newObjectPrefab = null;
                }
                else
                {
                    Debug.LogWarning("Prefab is not assigned.");
                }
            }
        }

        private void AddNewObjectToPool()
        {
            try
            {
                SimpleObjectPool objectPool = (SimpleObjectPool)target;

                ObjectPoolItem newItem = CreateInstance<ObjectPoolItem>();
                newItem.objectName = newObjectName;
                newItem.prefab = newObjectPrefab;
                newItem.poolSize = newObjectPoolSize;
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                    AssetDatabase.SaveAssets();
                    Debug.Log("Resources folder created");
                }
                if (!AssetDatabase.IsValidFolder("Assets/Resources/SimpleObjectPool"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources","SimpleObjectPool");
                    AssetDatabase.SaveAssets();
                    Debug.Log("SimpleObjectPool folder created");
                }
                AssetDatabase.CreateAsset(newItem, "Assets/Resources/SimpleObjectPool/" + newObjectName + ".asset");
                AssetDatabase.SaveAssets();

                var poolList = Resources.Load<ObjectPoolList>($"SimpleObjectPool/ObjectPoolList");
                if (poolList != null)
                {
                    poolList.ItemList.Add(newItem);
                    EditorUtility.SetDirty(poolList);
                }
                else
                {
                    poolList = CreateNewPoolList();
                    poolList.ItemList.Add(newItem);
                    EditorUtility.SetDirty(poolList);
                    Debug.Log("Pool List not found. Creating new Pool List Item");
                }

                SimpleObjectPool pool = (SimpleObjectPool)target;
                pool.poolItems = poolList;
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        private ObjectPoolList CreateNewPoolList()
        {
            ObjectPoolList poolList = CreateInstance<ObjectPoolList>();
            AssetDatabase.CreateAsset(poolList,"Assets/Resources/SimpleObjectPool/ObjectPoolList.asset");
            AssetDatabase.SaveAssets();
            return poolList;
        }
    }
}