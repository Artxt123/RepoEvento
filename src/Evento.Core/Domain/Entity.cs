using System;


namespace Evento.Core.Domain
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}


