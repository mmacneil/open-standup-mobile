﻿using System.Threading.Tasks;

namespace CleanXF.Core.Interfaces.Data.Repositories
{
    public interface ISessionRepository
    {
        Task<bool> Initialize(string accessToken);
    }
}
