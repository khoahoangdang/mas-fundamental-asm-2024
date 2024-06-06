using Microsoft.AspNetCore.Mvc;
using MSA.BankService.Domain;
using MSA.BankService.Infrastructure.Data;
using MSA.BankService.Dtos;
using MSA.Common.Contracts.Domain;
using MSA.Common.PostgresMassTransit.PostgresDB;

namespace MSA.BankService.Controllers;

[ApiController]
[Route("v1/bank")]
public class BankController : ControllerBase
{
    private readonly IRepository<Bank> repository;

    private readonly PostgresUnitOfWork<MainDbContext> uow;

    public BankController(
        IRepository<Bank> repository,
        PostgresUnitOfWork<MainDbContext> uow
        )
    {
        this.repository = repository;
        this.uow = uow;
    }

    [HttpGet]
    public async Task<IEnumerable<Bank>> GetAsync()
    {
        var Banks = (await repository.GetAllAsync()).ToList();
        return Banks;
    }

    [HttpPost]
    public async Task<ActionResult<Bank>> PostAsync(CreateBankDto createBankDto)
    {
        var Bank = new Bank { 
            Id = Guid.NewGuid(),
            Name = createBankDto.Name
        };
        await repository.CreateAsync(Bank);

        await uow.SaveChangeAsync();

        return CreatedAtAction(nameof(PostAsync), Bank);
    }
}