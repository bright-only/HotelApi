﻿using HotelListingApi.Data;
using System;
using System.Threading.Tasks;

namespace HotelListingApi.IRepository
{
  public interface IUnitOfWork : IDisposable
  {
    IGenericRepository<Country> Countries { get; }
    IGenericRepository<Hotel> Hotels { get; }
    Task Save();
  }
}
