﻿using System.Collections.Generic;
using System.Linq;

namespace PhysicsSample
{
    public class PhysicWorldAssociations<TAssociation> : IPhysicWorldAssociations<TAssociation> where TAssociation : IActuality
    {
        private struct PhysicalAssociation
        {
            public PhysicalAssociation(IPhysicalObject physicalObject, TAssociation associated)
            {
                PhysicalObject = physicalObject;
                Object = associated;
            }

            public TAssociation Object { get; }
            public IPhysicalObject PhysicalObject { get; }
        }

        private readonly List<PhysicalAssociation> _physicAssociations = new();
        private readonly IPhysicWorld _physicWorld;

        public PhysicWorldAssociations(IPhysicWorld physicWorld)
        {
            _physicWorld = physicWorld;
        }
        
        public bool HasAssociation(IPhysicalObject physicalObject)
        {
            return _physicAssociations.Any(association => association.PhysicalObject == physicalObject);
        }

        public TAssociation GetAssociation(IPhysicalObject physicalObject)
        {
            return _physicAssociations.First(association => association.PhysicalObject == physicalObject).Object;
        }

        public void Add(IPhysicalObject physicalObject, TAssociation associatedObject)
        {
            _physicWorld.Add(physicalObject);
            _physicAssociations.Add(new PhysicalAssociation(physicalObject, associatedObject));
        }

        public void Remove(IPhysicalObject key)
        {
            _physicWorld.Remove(key);
            _physicAssociations.RemoveAll(association => association.PhysicalObject == key);
        }

        public void RemoveAllInactual()
        {
            foreach (var association in _physicAssociations)
            {
                if (!association.Object.IsActual)
                {
                    _physicWorld.Remove(association.PhysicalObject);
                }
            }
            
            _physicAssociations.RemoveAll(association => !association.Object.IsActual);
        }
    }
}