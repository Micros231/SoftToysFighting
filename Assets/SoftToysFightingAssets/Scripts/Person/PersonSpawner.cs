using System;
using UnityEngine;
namespace Com.SoftToysFighting.Person
{
    public class PersonSpawner : MonoBehaviour
    {
        public GameObject PersonPrefab;
        public PersonType PersonType = PersonType.Player;
        public bool IsSpawnInStart = false;
        [SerializeField] private Color[] _colorsDrawGizmos = new Color[2];

        private void Awake()
        {
            InitPersonSpawner();
        }
        
        private void Start()
        {
            if (IsSpawnInStart)
            {
                Spawn();
            }
        }
        private void OnDrawGizmos()
        {
            switch (PersonType)
            {
                case PersonType.Player:
                    Gizmos.color = _colorsDrawGizmos[0];
                    break;
                case PersonType.Enemy:
                    Gizmos.color = _colorsDrawGizmos[1];
                    break;
            }
            Gizmos.DrawWireSphere(transform.position, 1);
        }

        public GameObject Spawn()
        {
            return Spawn(PersonPrefab);
        }
        public virtual GameObject Spawn(GameObject personPrefab)
        {
            if (personPrefab != null)
            {
                GameObject person = Instantiate(personPrefab, transform.position, Quaternion.identity);
                Debug.Log($"Spawn {person.name}");
                return person;
            }
            
            Debug.Log("Person prefab is null");
            return null;
        }

        protected virtual void InitPersonSpawner() { }
    }
}

