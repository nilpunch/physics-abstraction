﻿namespace GameLibrary.Sample.FullGame
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly IPhysicWorld<ICharacter> _charactersWorld;
        private readonly ICharacterViewFactory _characterViewFactory;

        public CharacterFactory(IPhysicWorld<ICharacter> charactersWorld, ICharacterViewFactory characterViewFactory)
        {
            _charactersWorld = charactersWorld;
            _characterViewFactory = characterViewFactory;
        }

        public ICharacter Create(int health, IWeapon weapon)
        {
            IPhysicalObject physicalObject =  new PhysicalObject(new SphereCollidingShell(
                new SphereShell(Vector3.Zero, new FloatingPoint()),
                new CollisionsLibrary()));

            ICharacter character = new Character(health, physicalObject, _characterViewFactory.Create(), weapon);

            _charactersWorld.Add(physicalObject, character);

            return character;
        }
    }
}