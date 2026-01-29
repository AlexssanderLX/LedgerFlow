using LedgerFlow.Entities.Enums;
using System.Net.NetworkInformation;

namespace LedgerFlow.Entities.Documents
{
    public abstract class Document
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public DateTimeOffset? ArchivedAt { get; private set; }
        public StatusDocument statusDocument { get; private set; }
        public string? DocumentNumber { get; private set; }


        // Constructor for all objects of type
        public Document( DateTime createdAt, string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("Title as invalid!", nameof(title));
            }
            if (createdAt > DateTimeOffset.UtcNow)
            {
                throw new ArgumentNullException("Create Date is invalid!", nameof(createdAt));
            }
            CreatedAt = createdAt;
            Title = title.Trim();
            statusDocument = StatusDocument.Draft;
        }

        
        public void Rename(string newTitle , DateTimeOffset now) 
        {
            EnsureNotArchived();
            EnsureEditable();

            if (string.IsNullOrWhiteSpace(newTitle)) 
                throw new ArgumentNullException("Title is invalid", nameof(newTitle));

            Title = newTitle.Trim();
            Touch(now);
        }

        // Method for activate a draft document using a document number reference
        public void Activate(DateTimeOffset now, string? documentNumber = null) 
        {
            EnsureNotArchived();

            if (statusDocument != StatusDocument.Draft)
                throw new InvalidOperationException("Only draft documents can be activated");

            if (!string.IsNullOrWhiteSpace(documentNumber))
                DocumentNumber = documentNumber.Trim();

            statusDocument = StatusDocument.Active;
            Touch(now);
        }
        // Method for cancel documents
        public void Cancel(DateTimeOffset now, int maxDaysAfterCreation = 7)
        {
            EnsureNotArchived();

            if (statusDocument != StatusDocument.Active)
                throw new InvalidOperationException("Only Active documents can be canceled");

            if ((now - CreatedAt).TotalDays > maxDaysAfterCreation)
                throw new InvalidOperationException($"Cannot cancel after {maxDaysAfterCreation} days");

            statusDocument = StatusDocument.Canceled;
            Touch(now);
        }

        public void Archive(DateTimeOffset now)
        {
            if (statusDocument == StatusDocument.Archived)
                return; // idempotente (chamar 2x não quebra)

            // normalmente só arquiva se já estiver finalizado/sem edição
            if (statusDocument == StatusDocument.Draft)
                throw new InvalidOperationException("Draft documents cannot be archived.");

            statusDocument = StatusDocument.Archived;
            ArchivedAt = now;
            Touch(now);
        }

        // -------- Helpers internos --------

        private void Touch(DateTimeOffset now) => UpdatedAt = now;

        private void EnsureNotArchived()
        {
            if (statusDocument == StatusDocument.Archived)
                throw new InvalidOperationException("Archived documents cannot be changed.");
        }

        private void EnsureEditable()
        {
            // regra clássica: depois de ativo, não edita (você pode ajustar)
            if (statusDocument != StatusDocument.Draft)
                throw new InvalidOperationException("Only Draft documents can be edited.");
        }
    }
}
