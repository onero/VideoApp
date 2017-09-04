using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Context;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    internal class VideoRepository : Repository<Video>
    {
        public VideoRepository(DbContext context) : base(context)
        {
        }
    }
}