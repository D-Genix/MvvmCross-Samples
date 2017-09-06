using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StarWarsSample.Core.Models;
using StarWarsSample.Core.Rest.Interfaces;
using StarWarsSample.Core.Services.Interfaces;

namespace StarWarsSample.Core.Services.Implementations
{
    public class SpeciesService : ISpeciesService
    {
        private readonly IRestClient _restClient;

        public SpeciesService (IRestClient restClient)
        {
            _restClient = restClient;
        }

        public Task<PagedResult<Specie>> GetSpeciesAsync(string url = null)
        {
            return string.IsNullOrEmpty(url)
                ? _restClient.MakeApiCall<PagedResult<Specie>>($"{Constants.BaseUrl}/species/", HttpMethod.Get)
                : _restClient.MakeApiCall<PagedResult<Specie>>(url, HttpMethod.Get);
        }

        public Task<Specie> GetSpecieAsync()
        {
            return _restClient.MakeApiCall<Specie>($"{Constants.BaseUrl}/species/3/", HttpMethod.Get);
        }

        private PagedResult<Specie> GetMockedSpecie()
        {
            return new PagedResult<Specie>()
            {
                Count = 3,
                Next = string.Empty,
                Previous = string.Empty,
                Results = new List<Specie>
                {
                    new Specie
                    {
                        Name = "Wookiee",
                        Average_Height = "210",
                        Language = "Shyriiwook"
                    },
                    new Specie
                    {
                        Name = "Hutt",
                        Average_Height = "300",
                        Language = "Huttese"
                    },
                    new Specie
                    {
                        Name = "Ewok",
                        Average_Height = "100",
                        Language =  "Ewokese"
                    }
                }
            };
        }
    }
}
