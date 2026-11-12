using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

public interface ITrendyolMemberApi
{
    [Get("/integration/member/countries")]
    Task<IApiResponse<List<TrendyolCountry>>> GetCountriesAsync();

    /// <summary>Cities of a GULF or CEE country. <paramref name="countryCode" /> is the code from <see cref="GetCountriesAsync" />.</summary>
    [Get("/integration/member/countries/{countryCode}/cities")]
    Task<IApiResponse<List<TrendyolLocation>>> GetCitiesByCountryAsync([AliasAs("countryCode")] string countryCode);

    /// <summary>Districts of a GULF country city. CEE countries have no district service.</summary>
    [Get("/integration/member/countries/{countryCode}/cities/{cityId}/districts")]
    Task<IApiResponse<List<TrendyolLocation>>> GetDistrictsByCityAsync(
        [AliasAs("countryCode")] string countryCode,
        [AliasAs("cityId")] long cityId
    );

    [Get("/integration/member/countries/domestic/AZ/cities")]
    Task<IApiResponse<List<TrendyolLocation>>> GetAzerbaijanCitiesAsync();

    [Get("/integration/member/countries/domestic/AZ/cities/{cityCode}/districts")]
    Task<IApiResponse<List<TrendyolLocation>>> GetAzerbaijanDistrictsAsync([AliasAs("cityCode")] long cityCode);

    [Get("/integration/member/countries/domestic/TR/cities")]
    Task<IApiResponse<List<TrendyolLocation>>> GetTurkeyCitiesAsync();

    [Get("/integration/member/countries/domestic/TR/cities/{cityCode}/districts")]
    Task<IApiResponse<List<TrendyolLocation>>> GetTurkeyDistrictsAsync([AliasAs("cityCode")] long cityCode);

    [Get("/integration/member/countries/domestic/TR/cities/{cityCode}/districts/{districtCode}/neighborhoods")]
    Task<IApiResponse<List<TrendyolLocation>>> GetTurkeyNeighborhoodsAsync(
        [AliasAs("cityCode")] long cityCode,
        [AliasAs("districtCode")] long districtCode
    );
}
