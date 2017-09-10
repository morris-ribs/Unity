using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
  private static T instance;
  public static T Instance
  {
      get {         
        if(instance == null) {
          // FindObjectOfType looks through the high hierarchy and going to find any instance of  
          //  a class of type T
          // and create an instance of it if the instance doesn't already exist
          // if the instance exists, it is going to return it
          instance = FindObjectOfType<T>();
        }
        else if (instance != FindObjectOfType<T>()) {
          Destroy(FindObjectOfType<T>());
        }
        DontDestroyOnLoad(FindObjectOfType<T>());
        return instance; 
      }
      set { instance = value; }
  }  
}
