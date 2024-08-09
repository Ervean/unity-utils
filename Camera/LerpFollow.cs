using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ervean.Utilities
{
    /// <summary>
    /// Follows a target, but lerped
    /// </summary>
    public class LerpFollow : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        [Header("Settings")]
        [SerializeField] private float speed = 1f;
        [SerializeField] private bool isFollowing = false;



        private void FixedUpdate()
        {
            
        }
    }
}