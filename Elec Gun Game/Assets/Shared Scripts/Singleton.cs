using UnityEngine;

namespace Singletons
{
    /// <summary>
    /// Singleton is a generic type I created that is given the type of the component that inherits this class.
    /// When a class inherits from singleton it can be the only instance of that class, if there are more an error is thrown.
    /// This allows for a single static instance of a class to be accessed without using serialized field since only one instance of the class will ever exist.
    /// This inherits from mono behaviour so any class that inherits from singleton still has that foundation.
    /// </summary>

    public class Singleton<T> : MonoBehaviour
        where T : Component
    {
        private static T _instance;
        public static T Instance    // This is a static variable accessed from the class definition of any classes that inherit from singleton
                                    // It is just a getter that returns a private instance variable set within the getter
        {
            get
            {
                // If the instance of the object hasn't been set yet, set it
                if (_instance == null)
                {
                    var objs = FindObjectsOfType(typeof(T)) as T[]; // Find all objects of the defined type
                    if (objs.Length > 0)                            // If there is more than zero, set the private instance to the first one found
                        _instance = objs[0];
                    if (objs.Length > 1)                            // If there is more than one, there is too many and an error is thrown to warn, does not crash simulation
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    if (_instance == null)                          // If none were found, meaning it wasn't set, create one on a new game object in the scene
                    {
                        GameObject obj = new GameObject();                  // Make the gameobejct
                        obj.name = string.Format("_{0}", typeof(T).Name);   // Name it
                        _instance = obj.AddComponent<T>();                  // Add the component
                    }
                }
                return _instance;   // Return the private instance set within the above code
            }
        }
    }
}