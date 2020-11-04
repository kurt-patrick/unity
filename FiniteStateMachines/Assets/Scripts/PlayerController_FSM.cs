using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FSM : MonoBehaviour
{
    private PlayerBaseState m_currentState;
    public PlayerBaseState CurrentState => m_currentState;

    private Rigidbody m_rigidbody;

    [SerializeField]
    internal int jumpForce;

    public Rigidbody Rigidbody => m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        TransitionToState(PlayerStates.IdleState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_currentState.OnColissionEnter(this);
    }

    public void TransitionToState(PlayerBaseState state)
    {
        m_currentState = state;
        m_currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_currentState.Update(this);
    }
}
