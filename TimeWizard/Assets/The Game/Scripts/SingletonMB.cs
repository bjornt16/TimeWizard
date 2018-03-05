using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMB<T> : MonoBehaviour {

    //Singleton
    private static SingletonMB<T> instance = null;
    public static SingletonMB<T> Instance { get { return instance; } }

    public abstract void CopyValues(SingletonMB<T> copy);

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("awake DEstroy!");
            this.CopyValues(instance);
            //Destroy(this.gameObject);
        }

        instance = this;       
    }
}
