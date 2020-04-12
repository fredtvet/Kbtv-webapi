﻿using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public abstract class UserDbSyncQuery<T> : IRequest<DbSyncResponse<T>> where T : DbSyncDto
    {
        public string FromDate { get; set; }
        public string UserName { get; set; }
    }
}
