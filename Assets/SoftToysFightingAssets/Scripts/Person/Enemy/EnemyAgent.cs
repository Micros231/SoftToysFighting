using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.SoftToysFighting.Person.Enemies
{
    public enum EnemyType { Enemy, Boss}
    public class EnemyAgent : MonoBehaviour
    {
        private enum EnemyActionState { Idling, Patrolling, Attacking, Relax }

        #region Public Properties
        public float DistanceToPlayer { get; private set; }
        public bool IsAttackHand { get; private set; }
        public bool IsAttackLeg { get; private set; }
        public bool IsAttackSuper { get; private set; }
        public bool IsJump { get; private set; }
        public Vector2 MoveToDirection { get; private set; }
        public Transform PlayerTransform { get; set; }
        public EnemyType EnemyType { get => _enemyType; set => _enemyType = value; }
        #endregion

        public bool IsEnemiesAttacking;

        #region Private SerializeFields
        [SerializeField] private float _distanceToStop = 0.5f;
        [SerializeField] private float _distanceToAttackX = 1.5f;
        [SerializeField] private float _distanceToAttackY = 0.5f;
        [SerializeField] private float _rangeToFindPlayer;
        [SerializeField] private float _rangeToExitAttackingZone;
        [SerializeField] private float _timeToPatrolling;
        [SerializeField] private float _timeToRelax;
        [SerializeField] private float _timeToNewPointRelax;
        [SerializeField] private float _timeToAttack;
        [SerializeField] private float _energyMultiplier;
        [SerializeField] private float _energyMax;
        [SerializeField] private int _countRoutePatrol;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private EnemyActionState _enemyActionState = EnemyActionState.Idling;
        #endregion

        #region Private Fields
        [SerializeField]
        private float[] _endZones = new float[4];
        private float _timer;
        private float _timerAttack;
        private float _timerRelax;
        private float _energyAmount;
        private int _numberRoutePatrol;
        private bool _isPlayerVisible;
        private bool _isFinishPatroll;
        private bool _isFindRoutePatroll;
        private bool _isStayAttackingZone;
        private bool _isFindRelaxPosition;
        private bool _isStayRelaxPoint;

        private List<EnemyAgent> _enemyAgents = new List<EnemyAgent>();
        private PersonAnimatorDragonBones _personAnimator;
        private Transform _transform;
        private Vector2 _routePosition;
        private Vector2 _relaxPosition;
        
        
            
        #endregion

        #region MonoBehaviours Callbacks
        private void Awake()
        {
            InitEnemyAgent();
        }
        private void Start()
        {
            InvokeRepeating("FindPlayer", 0, 0.5f);
        }
        private void Update()
        {
            if (_personAnimator.IsDead == false)
            {
                switch (_enemyActionState)
                {
                    case EnemyActionState.Idling:
                        IdlingAction();
                        break;
                    case EnemyActionState.Patrolling:
                        PatrollingAction();
                        break;
                    case EnemyActionState.Attacking:
                        AttackingAction();
                        break;
                    case EnemyActionState.Relax:
                        RelaxingAction();
                        break;
                    default:
                        break;
                }
                UpdateStates();
            }
            
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _rangeToFindPlayer);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _rangeToExitAttackingZone);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _distanceToAttackX);
        }

        #endregion

        #region Public Methods
        public void InitOfEnemiesController(float[] endZones, List<EnemyAgent> enemyAgents)
        {
            _endZones = endZones;
            _enemyAgents = enemyAgents;
        }
        #endregion

        #region Private Methods
        private void IdlingAction()
        {
            MoveToDirection = Vector2.zero;
        }
        private void PatrollingAction()
        {
            if (_numberRoutePatrol == _countRoutePatrol)
            {
                _numberRoutePatrol = 0;
                _isFinishPatroll = true;
                return;
            }
            if (_isFindRoutePatroll == false)
            {
                float horizontalPoint = Random.Range(_endZones[3], _endZones[2]);
                float verticalPoint = Random.Range(_endZones[1], _endZones[0]);
                _routePosition = new Vector2(horizontalPoint, verticalPoint);
                _isFindRoutePatroll = true;
            }
            MoveToDirection = GetDirectionToMove(_routePosition);
            if (Vector2.Distance(_transform.position, _routePosition) < 1)
            {
                _isFindRoutePatroll = false;
                _numberRoutePatrol++;
            }
        }
        private void AttackingAction()
        {
            ZeroingAttack();
            float distanceToPlayer = Vector2.Distance(_transform.position, PlayerTransform.position);
            float distanceY = _transform.position.y - PlayerTransform.position.y;
            if (distanceToPlayer > _distanceToStop && _isStayAttackingZone == false)
            {
                MoveToDirection = GetDirectionToMove(PlayerTransform.position);
            }
            else
            {
                _isStayAttackingZone = true;
                MoveToDirection = Vector2.zero;
                UpdateFlip();
            }
            if (distanceToPlayer > _rangeToExitAttackingZone)
            {
                _isStayAttackingZone = false;
            }
            if (distanceToPlayer <= _distanceToAttackX &&
               (distanceY > -_distanceToAttackY && distanceY < _distanceToAttackY))
            {
                _energyAmount = _energyAmount < _energyMax ? _energyAmount + _energyMultiplier * Time.deltaTime : _energyMax;
                _timerAttack += Time.deltaTime;
                if (_timerAttack >= _timeToAttack)
                {
                    GenerateAttack();
                    _timerAttack = 0;
                }
            }
            
        }
        private void RelaxingAction()
        {
            ZeroingAttack();
            float distanceToPlayer = Vector2.Distance(_transform.position, PlayerTransform.position);
            float distanceY = _transform.position.y - PlayerTransform.position.y;
            if (_isFindRelaxPosition == false)
            {
                float horizontalPoint;//Random.Range(_endZones[3], _endZones[2]);
                float verticalPoint = Random.Range(_endZones[1], _endZones[0]);  
                if (_transform.position.x > PlayerTransform.position.x)
                {
                    horizontalPoint = Random.Range(PlayerTransform.position.x + 2, _endZones[2]);
                }
                else
                    horizontalPoint = Random.Range(PlayerTransform.position.x - 2, _endZones[3]);                
                _relaxPosition = new Vector2(horizontalPoint, verticalPoint);
                _isFindRelaxPosition = true;
            }
            if (_isStayRelaxPoint == false)
            {
                MoveToDirection = GetDirectionToMove(_relaxPosition);
            }
            else
            {
                _timerRelax += Time.deltaTime;
                MoveToDirection = Vector2.zero;
                UpdateFlip();
            }
            if (_timerRelax >= _timeToNewPointRelax)
            {
                ZeroingRelaxing();
                return;
            }
            if (Vector2.Distance(_transform.position, _relaxPosition) < 1)
            {
                _isStayRelaxPoint = true;
            }
            if (distanceToPlayer <= _distanceToAttackX &&
               (distanceY > -_distanceToAttackY && distanceY < _distanceToAttackY))
            {
                _energyAmount = _energyAmount < _energyMax ? _energyAmount + _energyMultiplier * Time.deltaTime : _energyMax;
                _timerAttack += Time.deltaTime;
                if (_timerAttack >= _timeToAttack)
                {
                    GenerateAttack();
                    _timerAttack = 0;
                }
            }
        }
        private void GenerateAttack()
        {
            int randomNumberAttack;
            if (_enemyType == EnemyType.Enemy)
            {
                randomNumberAttack = Random.Range(0, 2);
            }
            else
            {
                randomNumberAttack = Random.Range(0, 3);
            }
            switch (randomNumberAttack)
            {
                case 0:
                    IsAttackHand = true;
                    break;
                case 1:
                    IsAttackLeg = true;
                    break;
                case 2:
                    if (_energyAmount >= _energyMax)
                    {
                        IsAttackSuper = true;
                        _energyAmount = 0;
                    }
                    break;
            }
        }
        private void UpdateStates()
        {
            switch (_enemyActionState)
            {
                case EnemyActionState.Idling:
                    _timer += Time.deltaTime;
                    if (_isPlayerVisible == false && _timer >= _timeToPatrolling)
                    {
                        _enemyActionState = EnemyActionState.Patrolling;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Idling.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    else if (_isPlayerVisible == true && IsEnemiesAttacking == false)
                    {
                        _enemyActionState = EnemyActionState.Attacking;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Idling.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    else if(_isPlayerVisible == true && IsEnemiesAttacking == true)
                    {
                        _enemyActionState = EnemyActionState.Relax;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Idling.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    break;
                case EnemyActionState.Patrolling:
                    if (_isPlayerVisible == false && _isFinishPatroll == true)
                    {
                        _enemyActionState = EnemyActionState.Idling;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Patrolling.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    else if(_isPlayerVisible == true && IsEnemiesAttacking == false)
                    {
                        _enemyActionState = EnemyActionState.Attacking;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Patrolling.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    else if(_isPlayerVisible == true && IsEnemiesAttacking == true)
                    {
                        _enemyActionState = EnemyActionState.Relax;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Patrolling.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    break;
                case EnemyActionState.Attacking:
                    _timer += Time.deltaTime;
                    if (_isPlayerVisible == false)
                    {
                        _enemyActionState = EnemyActionState.Idling;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Attacking.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    else if(_isPlayerVisible == true && IsEnemiesAttacking == true)
                    {
                        _enemyActionState = EnemyActionState.Relax;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Attacking.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    break;
                case EnemyActionState.Relax:
                    _timer += Time.deltaTime;
                    if (_isPlayerVisible == false)
                    {
                        _enemyActionState = EnemyActionState.Idling;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Relax.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    else if(_isPlayerVisible == true && IsEnemiesAttacking == false &&  (_timer >= _timeToRelax || _enemyAgents.Count > 1))
                    {
                        _enemyActionState = EnemyActionState.Attacking;
                        ZeroingAll();
                        Debug.Log($"Old state: {EnemyActionState.Relax.ToString()}. New state: {_enemyActionState.ToString()}");
                    }
                    break;
            }
        }
        private void UpdateFlip()
        {
            Vector2 direction = GetDirectionToMove(PlayerTransform.position);
            if (direction.x < 0)
            {
                _personAnimator.SetFlipX(true);
            }
            else if(direction.x > 0)
            {
                _personAnimator.SetFlipX(false);
            }
        }
        private void ZeroingAll()
        {
            ZeroingTimer();
            ZeroingPatrolling();
            ZeroingAttacking();
            ZeroingRelaxing();
            ZeroingAttack();

        }
        private void ZeroingTimer()
        {
            _timer = 0;
        }
        private void ZeroingPatrolling()
        {
            _numberRoutePatrol = 0;
            _isFindRoutePatroll = false;
            _isFinishPatroll = false;
            _routePosition = Vector2.zero;
            MoveToDirection = Vector2.zero;
        }
        private void ZeroingAttacking()
        {
            _isStayAttackingZone = false;
            _timerAttack = 0;
        }
        private void ZeroingRelaxing()
        {
            _isFindRelaxPosition = false;
            _isStayRelaxPoint = false;
            _timerRelax = 0;
            _relaxPosition = Vector2.zero;
        }
        private void ZeroingAttack()
        {
            IsAttackHand = false;
            IsAttackLeg = false;
            IsAttackSuper = false;
        }
        private void FindPlayer()
        {
            float distanceToPlayer = Vector2.Distance(_transform.position, PlayerTransform.position);
            _isPlayerVisible = distanceToPlayer < _rangeToFindPlayer ? true : false;
        }
        private void InitEnemyAgent()
        {
            _personAnimator = GetComponent<PersonAnimatorDragonBones>();
            _transform = GetComponent<Transform>();
        }
        private Vector2 GetDirectionToMove(Vector3 _target)
        {
            Vector2 heading = _target - _transform.position;
            float distanceToTarget = heading.magnitude;
            Vector2 direction = distanceToTarget > 0.1f ? heading / distanceToTarget : Vector2.zero;

            return direction;
        }
        #endregion
    }
    #region Legacy
    /*public Vector2 GetMoveToPlayer()
    {
        Vector2 heading = _player.position - _transform.position;
        DistanceToPlayer = heading.magnitude;
        Vector2 direction = DistanceToPlayer > _distanceToStop ? heading / DistanceToPlayer : Vector2.zero;

        return direction;
    }
    public bool AttackHand()
    {
        if (DistanceToPlayer <= _distanceToStop && IsAttackHand)
        {
            IsAttackHand = false;
            return true;
        }
        return false;
    }

    public bool AttackLeg()
    {
        if (DistanceToPlayer <= _distanceToStop && IsAttackLeg)
        {
            IsAttackLeg = false;
            return true;
        }
        return false;
    }*/
    #endregion
}

