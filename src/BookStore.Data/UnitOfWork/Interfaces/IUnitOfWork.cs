using System;

namespace BookStore.Data.UnitOfWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
}
