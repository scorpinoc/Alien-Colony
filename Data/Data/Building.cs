using System;
using Data.Data.Common;
using Data.Interfaces;

namespace Data.Data
{
    public class Building : IPhysicalObject
    {
        public string Name { get; }
        public Position Position { get; }
        public ObjectType ObjectType { get; }
        
        public Building(string name, Position position)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(Name));
            if (position == null)
                throw new ArgumentNullException(nameof(Position));

            Name = name;
            Position = position;
            ObjectType = ObjectType.Building;
        }
    }
}