using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 싱글톤
public class CSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    //! 인스턴스 프로퍼티
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                var gameObject = new GameObject(typeof(T).ToString());
                _instance = gameObject.AddComponent<T>();

                DontDestroyOnLoad(gameObject);
            }

            return _instance;
        }
    }

    //! 인스턴스를 생성한다
    public static T Create()
    {
        return Instance;
    }
}
