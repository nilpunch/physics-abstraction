﻿namespace GameLibrary.Physics
{
    public struct RaycastHit
    {
        public bool Occure { get; }
        
        public ContactPoint ContactPoint { get; }
        
        public IRigidbody Rigidbody { get; }
    }
}