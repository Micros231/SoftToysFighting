using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.Person
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PersonMovement : MonoBehaviour
    {
        #region Public Properties
        public Vector2 EndZonePointMax => _endZonePointMax;
        public Vector2 EndZonePointMin => _endZonePointMin;

        public Vector2 ColliderSize => _colliderSize;
        #endregion

        #region Public Fields
        public float SpeedMovement = 3f;
        #endregion

        #region Private SerializeFields
        [SerializeField]
        private Vector2 _endZonePointMax;
        [SerializeField]
        private Vector2 _endZonePointMin;
        [SerializeField]
        private Vector2 _colliderSize;
        #endregion

        #region Protected Fields
        protected Rigidbody2D _rigidbody2D;
        protected Collider2D _collider2D; 
        protected PersonAnimatorDragonBones _personAnimator;
        #endregion

        #region MonoBehaviour Callbacks
        private void Awake()
        {
            InitPersonMovement();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, _colliderSize * 2);
        }
        #endregion

        #region Public Methods
        public virtual void Move(Vector2 vectorInput, bool isJump)
        {
            if (!_personAnimator.IsAttack && !_personAnimator.IsDamageTake)
            {
                //Init
                Vector2 lastPosition = _rigidbody2D.position;
                Vector2 endZonePointMinUpdated = 
                    new Vector2(_endZonePointMin.x + _colliderSize.x, _endZonePointMin.y + _colliderSize.y);
                Vector2 endZonePointMaxUpdated =
                    new Vector2(_endZonePointMax.x - _colliderSize.x, _endZonePointMax.y - _colliderSize.y);

                //Logic
                vectorInput = Vector2.ClampMagnitude(vectorInput, 1f);
                Vector2 movement = vectorInput * SpeedMovement;
                Vector2 newPosition = lastPosition + movement * Time.fixedDeltaTime;
                Vector2 clampPosition = new Vector2(
                        Mathf.Clamp(newPosition.x, endZonePointMinUpdated.x, endZonePointMaxUpdated.x), 
                        Mathf.Clamp(newPosition.y, endZonePointMinUpdated.y, endZonePointMaxUpdated.y));
                _collider2D.enabled = !_personAnimator.IsJumping;

                //Final
                MoveAnimation(vectorInput, isJump);
                _rigidbody2D.MovePosition(clampPosition);
                
            }
            _rigidbody2D.velocity = Vector2.zero;
        }
        public void SetEndZonePoints(Vector2 min, Vector2 max)
        {
            _endZonePointMin = new Vector2(min.x, min.y);
            _endZonePointMax = new Vector2(max.x, max.y);    
        }
        #endregion

        #region Protected Methods
        protected virtual void InitPersonMovement()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _personAnimator = GetComponent<PersonAnimatorDragonBones>();
        }

        protected void MoveAnimation(Vector2 vectorInput, bool isJump)
        {
            _personAnimator.JumpAnimation(isJump);
            if (vectorInput.x == 0 && vectorInput.y == 0)
            {
                _personAnimator.MoveAnimation(0);
            }
            else if (vectorInput.x != 0 || vectorInput.y != 0)
            {
                if ((vectorInput.y > 0.4 || vectorInput.y < -0.4) && (vectorInput.x > -0.2f && vectorInput.x < 0.2f))
                {
                    _personAnimator.MoveAnimation(2);
                }
                else if (vectorInput.x > 0)
                {
                    _personAnimator.MoveAnimation(1);
                }
                else if (vectorInput.x < 0)
                {
                    _personAnimator.MoveAnimation(-1);
                }

            }
            if (vectorInput.x > 0)
            {
                _personAnimator.SetFlipX(false);
            }
            else if (vectorInput.x < 0)
            {
                _personAnimator.SetFlipX(true);
            }
        }
        #endregion
    }
}

