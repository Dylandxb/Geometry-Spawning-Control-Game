using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveList<T>
{
    private LinkedList<T> list;
    public LiveList()
    {
        list = new LinkedList<T>();
    }
    public void Add(T obj)
    {
        list.AddFirst(obj);
    }
    public void Remove(T obj)
    {
        try
        {
            list.Remove(obj);
        }
        catch (System.InvalidOperationException)
        {
            // ignore
        }
    }
    public T GetOldest()
    {
        // remove the oldest from the list and return it, return null if empty
        T retVal = default(T);
        if (list.Last != null)
        {
            retVal = list.Last.Value;
            list.RemoveLast();
        }
        return retVal;
    }
}
public class GeometryPool : MonoBehaviour
{
    public enum Policy
    {
        RETURN_NULL,
        REPLACE_OLDEST
    };
    [SerializeField] Policy policy;
    [SerializeField] int poolSize;
    [SerializeField] Transform prefab;
    [SerializeField] Transform capsulePrefab;
    [SerializeField] Transform circlePrefab;
    [SerializeField] Transform hexPointPrefab;
    [SerializeField] Transform hexBossPrefab;
    private GameObject[] pool;
    LinkedList<GameObject> freeList;
    LinkedList<GameObject> freeList1;
    LinkedList<GameObject> freeList2;
    LinkedList<GameObject> freeList3;
    LinkedList<GameObject> freeList4;
    LiveList<GameObject> liveList;
    void Start()
    {
        if (policy == Policy.REPLACE_OLDEST)
        {
            liveList = new LiveList<GameObject>();
        }
        freeList = new LinkedList<GameObject>();            //Diamond linkedList
        freeList1 = new LinkedList<GameObject>();           //Capsule linkedList
        freeList2 = new LinkedList<GameObject>();           //Circle linkedList
        freeList3 = new LinkedList<GameObject>();           //HexPoint linkedList
        freeList4 = new LinkedList<GameObject>();           //HexBoss linkedList
        // initialize the pool and spawn the objects
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(prefab).gameObject;
            pool[i].SetActive(false);
            freeList.AddFirst(pool[i]);                 //Add each object to beginning of linked list
        }
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(capsulePrefab).gameObject;
            pool[i].SetActive(false);
            freeList1.AddLast(pool[i]);                 //Add each object to beginning of linked list
        }

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(circlePrefab).gameObject;
            pool[i].SetActive(false);
            freeList2.AddLast(pool[i]);                 //Add each object to beginning of linked list
        }

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(hexPointPrefab).gameObject;
            pool[i].SetActive(false);
            freeList3.AddLast(pool[i]);                 //Add each object to beginning of linked list
        }

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(hexBossPrefab).gameObject;
            pool[i].SetActive(false);
            freeList4.AddLast(pool[i]);                 //Add each object to beginning of linked list
        }
    }

    public GameObject GetObject()
    {
        // TODO: Look for an unused object and return the first one found
        /*
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].activeInHierarchy)                                 //Free object, not active in hierarchy
            {
                return pool[i];                                             //Look for first obj and return it
            }
        }
        */
        if (freeList.First != null)                                         //checks if anything is in the linked list
        {
            GameObject obj = freeList.First.Value;              //Get value and return it
            freeList.RemoveFirst();                             //Remove it from list so same obj isnt returned every time
            if (policy == Policy.REPLACE_OLDEST)                                                    //Add it to livelist when its null
            {
                liveList.Add(obj);
            }
            return obj;                                     //First is linked list node, return the value on the node

        }
        else
        {
                                                                 //If no free obj, get oldest, add back to liveList and return it
        }
        {
            if ( policy == Policy.REPLACE_OLDEST)               //Only edit livelist if policy is replace_oldest
            {
                GameObject obj = liveList.GetOldest();          //Removes from the end of list
                liveList.Add(obj);                              //Adds to beginning
                return obj;
            }

        }
        return null;
    }
    //Create new getobject function
    //with different freeList
    //getting different object and adding it to the new freeList
    //call it in spawner

    public GameObject GetObjectCap()
    {
        if (freeList1.First != null)                                         //checks if anything is in the linked list
        {
            GameObject obj = freeList1.First.Value;              //Get value and return it
            freeList1.RemoveFirst();                             //Remove it from list so same obj isnt returned every time
            if (policy == Policy.REPLACE_OLDEST)                                                    //Add it to livelist when its null
            {
                liveList.Add(obj);
            }
            return obj;                                     //First is linked list node, return the value on the node

        }
        else
        {
            //If no free obj, get oldest, add back to liveList and return it
        }
        {
            if (policy == Policy.REPLACE_OLDEST)               //Only edit livelist if policy is replace_oldest
            {
                GameObject obj = liveList.GetOldest();          //Removes from the end of list
                liveList.Add(obj);                              //Adds to beginning
                return obj;
            }

        }
        return null;
    }

    public GameObject GetObjectCircle()
    {
        if (freeList2.First != null)                                         //checks if anything is in the linked list
        {
            GameObject obj = freeList2.First.Value;              //Get value and return it
            freeList2.RemoveFirst();                             //Remove it from list so same obj isnt returned every time
            if (policy == Policy.REPLACE_OLDEST)                                                    //Add it to livelist when its null
            {
                liveList.Add(obj);
            }
            return obj;                                     //First is linked list node, return the value on the node

        }
        else
        {
            //If no free obj, get oldest, add back to liveList and return it
        }
        {
            if (policy == Policy.REPLACE_OLDEST)               //Only edit livelist if policy is replace_oldest
            {
                GameObject obj = liveList.GetOldest();          //Removes from the end of list
                liveList.Add(obj);                              //Adds to beginning
                return obj;
            }

        }
        return null;
    }

    public GameObject GetObjectHexPoint()
    {
        if (freeList3.First != null)                                         //checks if anything is in the linked list
        {
            GameObject obj = freeList3.First.Value;              //Get value and return it
            freeList3.RemoveFirst();                             //Remove it from list so same obj isnt returned every time
            if (policy == Policy.REPLACE_OLDEST)                                                    //Add it to livelist when its null
            {
                liveList.Add(obj);
            }
            return obj;                                     //First is linked list node, return the value on the node

        }
        else
        {
            //If no free obj, get oldest, add back to liveList and return it
        }
        {
            if (policy == Policy.REPLACE_OLDEST)               //Only edit livelist if policy is replace_oldest
            {
                GameObject obj = liveList.GetOldest();          //Removes from the end of list
                liveList.Add(obj);                              //Adds to beginning
                return obj;
            }

        }
        return null;
    }

    public GameObject GetObjectHexBoss()
    {
        if (freeList4.First != null)                                         //checks if anything is in the linked list
        {
            GameObject obj = freeList4.First.Value;              //Get value and return it
            freeList4.RemoveFirst();                             //Remove it from list so same obj isnt returned every time
            if (policy == Policy.REPLACE_OLDEST)                                                    //Add it to livelist when its null
            {
                liveList.Add(obj);
            }
            return obj;                                     //First is linked list node, return the value on the node

        }
        else
        {
            //If no free obj, get oldest, add back to liveList and return it
        }
        {
            if (policy == Policy.REPLACE_OLDEST)               //Only edit livelist if policy is replace_oldest
            {
                GameObject obj = liveList.GetOldest();          //Removes from the end of list
                liveList.Add(obj);                              //Adds to beginning
                return obj;
            }

        }
        return null;
    }
    public void ReturnObject(GameObject o)
    {
        // TODO: set this object to be unused
        o.SetActive(false);                                                 //Active in hierarchy to false
        freeList.AddFirst(o);                                               //add object back to freeList
        freeList1.AddFirst(o);                                               //add object back to freeList
        freeList2.AddFirst(o);                                               //add object back to freeList
        freeList3.AddFirst(o);                                               //add object back to freeList
        freeList4.AddFirst(o);                                               //add object back to freeList
        if (policy == Policy.REPLACE_OLDEST)
        {
            liveList.Remove(o);
        }
    }
}