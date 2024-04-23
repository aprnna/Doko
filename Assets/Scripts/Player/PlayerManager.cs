using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        private FirebaseManager _pm;

        // private Dictionary<string, bool> _trashes = new Dictionary<string, bool>

        [SerializeField] private int _coin;
        public Dictionary<string, bool> trashes = new Dictionary<string, bool>()
        {
            { "botol", false },
            { "botolkaca", false },
            { "buah", false },
            { "daunkering", false },
            { "kaleng", false },
            { "kardus", false },
            { "kertas", false },
            { "sayur", false }
        };

        public int Coin
        {
            get
            {
                return _coin;
            }
            set
            {
                _coin = value;
                _pm.SaveData();
                Debug.Log("Berhasil Data");
            }
        }

        public void SetTrash(string key, bool value)
        {
            if (trashes.ContainsKey(key))
            {
                trashes[key] = value;
            }
            else
            {
                trashes.Add(key, value);
            }
        }

        public bool GetTrash(string key)
        {
            bool result = false;

            if (trashes.ContainsKey(key))
            {
                result = trashes[key];
            }

            return result;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _pm = FirebaseManager.Instance;
        }
    }
}
