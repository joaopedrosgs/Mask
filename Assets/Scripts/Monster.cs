using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    enum State
    {
        Idle,
        Following,
        Attacking,
        Rage
    }
    

    private UnityEngine.AI.NavMeshAgent _nav;
    private Animator _anim;
    private GameObject _target;
    private SphereCollider _collider;
    private State _state = State.Idle;
    private DateTime _lastMovement = DateTime.Now;
    public float Delay = 5f;
    public Transform pPosition;
    void Awake()
    {
        _nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<SphereCollider>();
        Vector3 newPosition = transform.position;
        newPosition.y = Terrain.activeTerrain.SampleHeight(newPosition);
        transform.position = newPosition;
        pPosition = GameObject.Find("ThirdPersonController").transform;


    }
    // Use this for initialization
    private void OnTriggerStay(Collider target)
    {
        if (!_target)
        {
            if (!target.CompareTag("Player"))
                return;

            _target = target.gameObject;
            _state = State.Following;
            transform.LookAt(transform.position);
            Delay = 1f;
                
        }
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, pPosition.position) > 30)
            return;
        if(_target)
            transform.LookAt(transform.position);

        _anim.SetFloat("Speed", _nav.velocity.magnitude);
        if (_lastMovement.AddSeconds(Delay) > DateTime.Now)
            return;

        switch (_state)
        {
            case State.Idle:
                {
                    _nav.speed = 2f;
                    Delay = UnityEngine.Random.Range(6,12);
                    randomWalk();
                }
                break;
            case State.Following:
                {
                    Delay = 0.3f;
                    _nav.speed = _nav.speed < 4 ? 4 : _nav.speed;
                    if (_target)
                    {
                        if (_collider.bounds.Contains(_target.transform.position))
                        {
                            if (Vector3.Distance(transform.position, _target.transform.position) <= 2)
                                _state = State.Attacking;
                            else
                                _nav.SetDestination(_target.transform.position);
                        }
                        else
                        {
                            _target = null;
                            _state = State.Idle;
                        }
                    }

                }
                break;
            case State.Attacking:
                {
                    
                    if (Vector3.Distance(transform.position, _target.transform.position) > 2)
                        _state = State.Rage;
                    else
                    {
                        _anim.SetTrigger("Attack");
                        Delay = 1.5f;
                    }
                }
                break;
            case State.Rage:
                {
                    _anim.SetTrigger("Rage");
                    _state = State.Following;
                    Delay = 1f;
                    _nav.speed = 6;
                }
                break;
            default:
                _state = State.Idle;
                break;



        }
        _lastMovement = DateTime.Now;
        _anim.SetFloat("Speed", _nav.velocity.magnitude);
        _anim.SetInteger("AttackType", UnityEngine.Random.Range(0, 3));



    }

    private void randomWalk()
    {
        Debug.Log("random");
        Vector3 newPosition = new Vector3(UnityEngine.Random.Range(-10, 10), 0,  UnityEngine.Random.Range(-10, 10));
        newPosition += transform.position;
        Debug.Log(newPosition);
        _nav.destination = newPosition;


    }
    public void Attack() {
        var Status = _target.GetComponent<PlayerStatus>();
        Status.TakeDamage(UnityEngine.Random.Range(20,30));
    }

    // Update is called once per frame

   
    void LateUpdate()
    {


    }
}
