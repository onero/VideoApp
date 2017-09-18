﻿using VideoAppDAL.Context;
using VideoAppDAL.Interfaces;
using VideoAppDAL.UnitOfWork;

namespace VideoAppDAL
{
    public class DALFacade : IDALFacade
    {
        public IUnitOfWork UnitOfWork => new UnitOfWork.UnitOfWork(new SQLContext());
    }
}