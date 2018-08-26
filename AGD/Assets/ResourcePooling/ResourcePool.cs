using UnityEngine;
using System.Collections.Generic;

public class ResourcePool : MonoBehaviour {
	
	string _resourcePath;
    GameObject _resourceObject;

    List<GameObject> _usedInstances;
	List<GameObject> _unusedInstances;
	
	public void Init(string resourcePath, GameObject resourceObject = null) {
		_resourcePath = resourcePath;
        _resourceObject = resourceObject;
        _usedInstances = new List<GameObject>();
		_unusedInstances = new List<GameObject>();
	}
	
	/* Returns an unused existing instance, or creates a new one if necessary */
	public GameObject GetUnusedInstance() {
		GameObject instance;

        int count = _unusedInstances.Count;
		if (count > 0) {
			instance = _unusedInstances[count - 1];
			instance.SetActive(true);
            _usedInstances.Add(instance);
			_unusedInstances.RemoveAt(count - 1);
			
			//Debug.Log("reusing resource: " + instance.name + "; instances left: " + (count - 1));
		} else {
            if (ResourcesManager.Instance.logInstantiatesAndDestroys)
                Debug.Log("Instantiate: " + _resourcePath);
			instance = Instantiate(_resourceObject == null ? (_resourceObject = Resources.Load(_resourcePath) as GameObject) : _resourceObject) as GameObject;
            _usedInstances.Add(instance);
			
			// Add a PoolableResource component and set a reference to this ResourcePool
			PoolableResource poolableResource = instance.AddComponent<PoolableResource>();
			poolableResource.resourcePool = this;

            //Debug.Log("new resource: " + instance.name + "; instances left: " + _unusedInstances.Count);
		}
		
		return instance;
	}
	
	/* Disables the specified GameObject and adds it to the pool of unused instances */
    public void FreeInstance(GameObject instance)
    {
        instance.transform.SetParent(ResourcesManager.Instance.poolingObjects, false);
		instance.SetActive(false);
        _usedInstances.Remove(instance);
		_unusedInstances.Add(instance);
	}

    /* Setting all the Freeing all the instances in a Rescource pool. So all instances are gone but not deleted from program */
    public void FreeResourcePool()
    {
        while(_usedInstances.Count>0)
        {
            FreeInstance(_usedInstances[_usedInstances.Count-1]);
        }
    }
    public void ClearAndDestroy()
    {
        foreach (GameObject instance in _usedInstances)
            RemoveInstance(instance);
        foreach (GameObject instace in _unusedInstances)
            RemoveInstance(instace);
        Destroy(this);
    }

    private void RemoveInstance(GameObject instance)
    {
        try
        {
            Destroy(instance);
        }catch(MissingReferenceException mre){
            print("MRE!= " + mre.Message);
            //print("Instance: "+instance.name);
        }
    }
}
