using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour
{
    public float m_Radius;
    public Transform m_Target;
    public float m_Speed;
    public float m_TurnSpeed;
    public float m_Mass;
    public Transform m_ParticleSystem;
    public float m_ParticleSystemDuration = 5.0f;

    private void Update()
    {
        Vector3 velocity = transform.forward * m_Speed * Time.deltaTime;

        if (m_Target)
        {
            Vector3 desiredVelocity = m_Target.position - transform.position;
            float distance = desiredVelocity.magnitude;

            if (m_Radius > distance)
            {
                desiredVelocity = desiredVelocity.normalized * m_Speed * Time.deltaTime;
                Vector3 steeringForce = desiredVelocity - velocity;
                steeringForce = steeringForce.normalized * m_TurnSpeed * Time.deltaTime;
                Vector3 acceleration = steeringForce / m_Mass;
                velocity += acceleration;
            }
        }

        transform.position += velocity;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        m_ParticleSystem.parent = null;
        Destroy(m_ParticleSystem.gameObject, m_ParticleSystemDuration);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
