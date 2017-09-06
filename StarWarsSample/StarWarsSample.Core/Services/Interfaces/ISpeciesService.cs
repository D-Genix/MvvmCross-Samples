using System;
using System.Threading.Tasks;
using StarWarsSample.Core.Models;

namespace StarWarsSample.Core.Services.Interfaces
{
    public interface ISpeciesService
    {
        Task<PagedResult<Specie>> GetSpeciesAsync(string url = null);

        Task<Specie> GetSpecieAsync();
    }
}
