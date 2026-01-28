using LedgerFlow.Entities;
using LedgerFlow.Entities.Enums;


namespace LedgerFlow.Models
{
    public class ClientV1
    {
        public string Name { get; private set; }
        public Document Document { get; private set; }
        public TaxRegime? TaxRegime { get; private set; }
        public bool HasEmployes { get; private set; }
        public bool HasProlabour { get; private set; }
        public int Id { get; private set; }
        public Enterprise? Enterprise { get; private set; }

        public ClientV1(string name, Document document, int id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Nome e documento são obrigatórios");
            }
            Name = name;
            Document = document;
            Id = id;
        }
    }
}
