using UnityEngine;
using System.Collections.Generic;

public class ResourcesManager : Singleton<ResourcesManager> {
	
	Dictionary<string, ResourcePool> _resourcePools;
    [HideInInspector]
    public Transform poolingObjects;

    [HideInInspector]
    public bool logInstantiatesAndDestroys = false;
	
	override public void Init() {
		_resourcePools = new Dictionary<string, ResourcePool>();
        poolingObjects = GameObject.Find("PoolingObjects").transform;
	}
	
	/* Register the specified resource as poolable and create a ResourcePool for it */
	public void RegisterPool(string resourcePath, GameObject resourceObject = null, int prefillSize = 0) {
		if (_resourcePools.ContainsKey(resourcePath)) {
			return;
		}
		
		// ResourcePools are added as components since they make use of MonoBehaviour functionality
		ResourcePool resourcePool = gameObject.AddComponent<ResourcePool>();
		resourcePool.Init(resourcePath, resourceObject);
		_resourcePools.Add(resourcePath, resourcePool);

        if (prefillSize > 0)
        {
            GameObject[] gosInCache = new GameObject[prefillSize];

            // Fill the cache
            for (int j = 0; j < prefillSize; ++j)
            {
                GameObject go = GetResourceInstance(resourcePath, resourceObject);
                gosInCache[j] = go;
            }

            // Deactive the objects
            for (int j = 0; j < prefillSize; ++j)
                RemoveResourceInstance(gosInCache[j]);
        }
	}

    /* Deregister the specified resource and destroy all objects of it */
    public void DeregisterPool(string resourcePath)
    {
        if (_resourcePools.ContainsKey(resourcePath))
        {
            _resourcePools[resourcePath].ClearAndDestroy();
            _resourcePools.Remove(resourcePath);
        }
    }

    /* Creates an instance of the specified resource or gets one from the corresponding ResourcePool */
    public GameObject GetResourceInstance(string resourcePath, GameObject resourceObject = null) {
        if (_resourcePools.ContainsKey(resourcePath)) {
            return _resourcePools[resourcePath].GetUnusedInstance();
        }

        if (logInstantiatesAndDestroys)
            Debug.Log("Instantiate: " + resourcePath);

        if (resourceObject == null)
        {
            resourceObject = Resources.Load(resourcePath) as GameObject;
            if (resourceObject == null)
            {
                Debug.LogError("ResourceObject not found: " + resourcePath);
                return null;
            }
        }

        return Instantiate(resourceObject) as GameObject;
	}
	
	/* Destroys the specified GameObject, or disables it and adds it to the available instances of the corresponding ResourcePool */
	public void RemoveResourceInstance(GameObject instance, bool deactivateIfNotExists = false) {
        PoolableResource poolableResource = instance.GetComponent<PoolableResource>();
		if (poolableResource != null) {
			poolableResource.resourcePool.FreeInstance(instance);
		} else {
            if (deactivateIfNotExists)
            {
                instance.SetActive(false);
            }
            else
            {
                if (logInstantiatesAndDestroys)
                    Debug.Log("Destroy: " + instance.name);
                Destroy(instance);
            }
		}
	}
    /* Freeing all the instances in a Rescource pool. So all instances are gone but not deleted from program */
    public void FreeResource(string resourcePath)
    {
        if (_resourcePools.ContainsKey(resourcePath))
        {
            _resourcePools[resourcePath].FreeResourcePool();
        }
    }
}
