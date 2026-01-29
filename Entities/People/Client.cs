using LedgerFlow.Entities.Documents;
using LedgerFlow.Entities.Enums;
using LedgerFlow.Entities.Organizations;


namespace LedgerFlow.Entities.People
{
    public abstract class Client
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public Client(string name, DateTimeOffset createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name is mandatory!");
            if (createdAt > DateTimeOffset.UtcNow)
                throw new ArgumentOutOfRangeException("A create date has being in future!");
            Name = name.Trim();
            CreatedAt = createdAt;
            UpdatedAt = null;
        }

        public void Rename(string newName, DateTimeOffset now)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentNullException("The field cant stay null or white space!");
            if (now > DateTimeOffset.UtcNow)
                throw new ArgumentOutOfRangeException("The New date not can be in future!");
            if (now < CreatedAt)
                throw new ArgumentOutOfRangeException("The New date not can be created before your cration date!");
            Name = newName.Trim();
            UpdatedAt = now;
        }
    }
}
