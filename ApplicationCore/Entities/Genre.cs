using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class Genre : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
