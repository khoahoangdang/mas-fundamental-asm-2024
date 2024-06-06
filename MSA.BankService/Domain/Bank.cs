using MSA.Common.Contracts.Domain;

namespace MSA.BankService.Domain
{
    public class Bank : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}