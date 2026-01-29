namespace LedgerFlow.Entities.Organizations
{
    public class Enterprise
    {
        public string Cnpj { get; private set; } = "";
        public double Expenses { get; private set; }
        public double Revenue { get; private set; }
        public double Invoicing { get; private set; }

        public Enterprise(string cnpj, double expenses, double revenue, double invoicing)
        {

            Cnpj = cnpj;
       
        }
    
    }
}
