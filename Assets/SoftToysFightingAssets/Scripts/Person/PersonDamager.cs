using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Com.SoftToysFighting.Person
{
    public enum PersonType { Player, Enemy }
    [RequireComponent(typeof(BoxCollider2D))]
    public class PersonDamager : MonoBehaviour
    {
        public float DistanceYToDamage = 0.5f;
        [SerializeField]
        private PersonType _personType;

        private List<PersonParameters> _personParameters = new List<PersonParameters>();
        private PersonAnimatorDragonBones _personAnimator;
        private Transform _parentTransform;
        private float _damage;
        private bool _isAttack { get => _personAnimator.IsAttack; }
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponentInParent<PersonParameters>())
            {
                PersonParameters personParameters = collision.GetComponentInParent<PersonParameters>();
                Vector3 postition = personParameters.transform.position;
                if (postition.y > _parentTransform.position.y - DistanceYToDamage &&
                    postition.y < _parentTransform.position.y + DistanceYToDamage)
                {
                    switch (_personType)
                    {
                        case PersonType.Player:
                            personParameters.TakeDamage(_damage);
                            break;
                        case PersonType.Enemy:
                            if (personParameters.gameObject.tag == "Player")
                            {
                                personParameters.TakeDamage(_damage);
                            }
                            break;
                    }
                }
                
            }
        }
        public Collider2D InitPersonDamager(float damage, Transform parentTransform, PersonAnimatorDragonBones personAnimator)
        {
            _damage = damage;
            _parentTransform = parentTransform;
            _personAnimator = personAnimator;
            return GetComponent<Collider2D>();
        }
    }
}

