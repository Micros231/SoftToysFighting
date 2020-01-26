using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.Person.Enemies.AI
{
    public enum EnemyType { Enemy, Boss}
    public class EnemyAIAgent : MonoBehaviour
    {
        #region Public Properties
        public bool IsAttackHand { get => _isAttackHand; }
        public bool IsAttackLeg { get => _isAttackLeg; }
        public bool IsAttackSuper { get => _isAttackSuper; }
        public bool IsJump { get => _isJump; }
        public bool IsAction { get => _isAction; set => _isAction = value; }
        public bool IsTargetVisible { get => _isTargetVisible; }
        public float MaxEnergy { get => _maxEnergy; } 
        public float Energy { get => _energy; }
        public Vector2 MoveToDirection { get => _moveToDirection; }
        public Vector2 AttackingZoneSize { get => _attackingZoneSize; }
        public EnemyType EnemyType { get => _enemyType; }
        public Transform TargetTransform { get => _targetTransform; }
        #endregion

        #region Private SerializeFields
        [Header("Output Data")]
        [SerializeField] private bool _isAttackHand;
        [SerializeField] private bool _isAttackLeg;
        [SerializeField] private bool _isAttackSuper;
        [SerializeField] private bool _isJump;
        [SerializeField] private bool _isAction;
        [SerializeField] private bool _isTargetVisible;
        [SerializeField] private Vector2 _moveToDirection;
        [Header("Settings")]
        [SerializeField] private float _rangeToFindTarget;
        [SerializeField] private Vector2 _attackingZoneSize;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Vector2 _endZonePointMin;
        [SerializeField] private Vector2 _endZonePointMax;
        [SerializeField] private float _maxEnergy;
        [SerializeField] private float _energy;
        [SerializeField] private float _energyMultiplier;
        #endregion

        #region Private Fields
        private Transform _transform;
        private PersonAnimatorDragonBones _personAnimator;
        private EnemyAIComponent _enemyAIComponent;
        private Vector2 _movePosition;
        #endregion

        #region MonoBehaviour Callbacks
        private void Awake()
        {
            InitEnemyAIAgent();
        }
        private void Start()
        {
            InvokeRepeating(nameof(FindTarget), 0, _enemyAIComponent.TimeBetweenRefresh);
        }

        #endregion

        #region Public Methods
        public void InitAIAgent(Transform targetTransform, EnemyType enemyType, Vector2 endZonePointMin, Vector2 endZonePointMax)
        {
            _targetTransform = targetTransform;
            _enemyType = enemyType;
            _endZonePointMin = endZonePointMin;
            _endZonePointMax = endZonePointMax;
        }
        public void Idle()
        {
            _moveToDirection = Vector2.zero;
        }
        public void PatrolingMove()
        {
            if (_moveToDirection == Vector2.zero)
            {
                float horizontalPoint = Random.Range(_endZonePointMin.x, _endZonePointMax.x);
                float verticalPoint = Random.Range(_endZonePointMin.y, _endZonePointMax.y);
                _movePosition = new Vector2(horizontalPoint, verticalPoint);
            }
            _moveToDirection = GetDirectionToMove(_movePosition);
        }
        public void MoveToTarget()
        {
            Vector2 enemyPosition = _transform.position;
            Vector2 targetPosition = _targetTransform.position;
            if (targetPosition.x > enemyPosition.x + _attackingZoneSize.x || 
                targetPosition.x < enemyPosition.x - _attackingZoneSize.x)
            {
                _moveToDirection = GetDirectionToMove(targetPosition);
            }
            else if (targetPosition.y > enemyPosition.y + _attackingZoneSize.y ||
                    targetPosition.y < enemyPosition.y - _attackingZoneSize.y)
            {
                _moveToDirection = GetDirectionToMove(targetPosition);
            }
            else
            {
                _moveToDirection = Vector2.zero;
            }
        }
        public void AttackHand()
        {
            _isAttackHand = true;
            
        }
        public void AttackLeg()
        {
            _isAttackLeg = true;
        }
        public void AttackSuper()
        {
            _isAttackSuper = true;
            _energy = 0;
        }
        public void Jump()
        {
            _isJump = true;
        }
        public void Reload()
        {
            _isAttackHand = false;
            _isAttackLeg = false;
            _isAttackSuper = false;
            _isJump = false;
            _isAction = false;
            _moveToDirection = Vector2.zero;
        }
        public void UpdateFlip()
        {
            Vector2 direction = GetDirectionToMove(TargetTransform.position);
            if (direction.x < 0)
            {
                _personAnimator.SetFlipX(true);
            }
            else if (direction.x > 0)
            {
                _personAnimator.SetFlipX(false);
            }
        }
        #endregion

        #region Private Methods
        private void InitEnemyAIAgent()
        {
            _transform = GetComponent<Transform>();
            _personAnimator = GetComponent<PersonAnimatorDragonBones>();
            _enemyAIComponent = GetComponent<EnemyAIComponent>();
        }
        private void FindTarget()
        {
            if (_targetTransform != null)
            {
                Vector2 heading = TargetTransform.position - _transform.position;
                float distanceToTarget = heading.magnitude;
                _isTargetVisible = distanceToTarget < _rangeToFindTarget ? true : false;
                if (_enemyType == EnemyType.Boss)
                {
                    _energy = _energy < _maxEnergy ? _energy + _energyMultiplier : _maxEnergy;
                }
            }
            else if (_targetTransform == null)
            {
                _isTargetVisible = false;
            }
        }
        
        private Vector2 GetDirectionToMove(Vector3 _target)
        {
            Vector2 heading = _target - _transform.position;
            float distanceToTarget = heading.magnitude;
            Vector2 direction = distanceToTarget > 0.1f ? heading / distanceToTarget : Vector2.zero;

            return direction;
        }
        #endregion

        #region Gizoms Callbacks
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _rangeToFindTarget);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, _attackingZoneSize);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_movePosition, 0.5f);
        }
        #endregion
    }
}

