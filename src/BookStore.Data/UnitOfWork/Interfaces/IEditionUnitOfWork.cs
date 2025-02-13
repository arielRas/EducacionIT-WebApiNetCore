using System;
using BookStore.Data.Repository.Interfaces;

namespace BookStore.Data.UnitOfWork.Interfaces;

public interface IEditionUnitOfWork : IUnitOfWork
{
    IEditionRepository EditionRepository {get;}
    IEditionPriceRepository EditionPriceRepository {get;}
    IEditionStockRepository EditionStockRepository{get;}
}
