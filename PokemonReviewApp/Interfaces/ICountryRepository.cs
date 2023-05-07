using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
	public interface ICountryRepository
	{
        ICollection<Country> GetCountries();
        Country GetCountry(long countryId);
        Country GetCountryByOwner(long ownerId);
        ICollection<Owner> GetOwnersFromACountry(long countryId);
        bool CountryExists(long countryId);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();
    }
}

