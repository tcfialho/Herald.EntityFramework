using System;

namespace Herald.EntityFramework.Tests
{
    public class EntityStub
    {
        protected EntityStub()
        {
        }

        public EntityStub(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
    }
}