using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    //[SerializeField] private Vector3 _offset;
    [SerializeField] private float _moventSpeed;
    [SerializeField] private bool _useRotation;
    [SerializeField] private float _rotationSpeed;

    Quaternion originRot;

    private void Start()
    {
        originRot = transform.rotation;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position , _moventSpeed * Time.deltaTime);

        if (_useRotation)
        {
            var resultRot = Quaternion.Slerp(transform.rotation, _target.rotation, _rotationSpeed * Time.deltaTime);
            transform.rotation = resultRot;
        }
        else
        {
            //transform.rotation = originRot;
        }
    }
}