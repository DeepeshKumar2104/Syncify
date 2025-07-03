using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncify.Domain.AwsEntities;

namespace Syncify.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAddress(Address address);
    }
}
