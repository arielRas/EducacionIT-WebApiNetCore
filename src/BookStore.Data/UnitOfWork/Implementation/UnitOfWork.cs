using System;
using BookStore.Data.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookStore.Data.UnitOfWork.Implementation;

public abstract class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext 
{
    protected readonly TContext _context;
    protected IDbContextTransaction? _transaction;

    public UnitOfWork(TContext context)
        => _context = context;


    public async Task BeginTransactionAsync()
        => _transaction ??= await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
    {
        if(IsTransactionActive())
        {
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();  
            _transaction?.Dispose();  
            _transaction = null;       
        }            
    }

    public async Task RollbackTransactionAsync()
    {
        if(IsTransactionActive())
        {
            await _transaction!.RollbackAsync();
            _transaction?.Dispose();
            _transaction = null;
        }            
    }

    private bool IsTransactionActive()
        => _transaction is not null && _transaction.GetDbTransaction().Connection is not null;  
}
