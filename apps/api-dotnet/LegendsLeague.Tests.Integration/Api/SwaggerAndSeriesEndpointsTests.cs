using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LegendsLeague.Contracts.Series;
using LegendsLeague.Tests.Integration.Support;
using Xunit;

namespace LegendsLeague.Tests.Integration.Api;

/// <summary>
/// Integration tests that boot the API and hit real endpoints (Swagger + Series).
/// </summary>
public class SwaggerAndSeriesEndpointsTests : IClassFixture<TestWebAppFactory>
{
    private readonly HttpClient _client;

    public SwaggerAndSeriesEndpointsTests(TestWebAppFactory factory)
    {
        _client = factory.CreateClient(new()
        {
            BaseAddress = new Uri("http://localhost")
        });
    }

    [Fact]
    public async Task SwaggerJson_is_served()
    {
        var resp = await _client.GetAsync("/swagger/v1/swagger.json");
        resp.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await resp.Content.ReadAsStringAsync();

        json.Should().Contain("\"title\": \"Legends League API\"");
        json.Should().Contain("\"version\": \"v1\"");
    }

    [Fact]
    public async Task GetSeries_returns_seeded_rows()
    {
        var resp = await _client.GetAsync("/api/v1/series");
        resp.StatusCode.Should().Be(HttpStatusCode.OK);

        var rows = await resp.Content.ReadFromJsonAsync<IReadOnlyList<SeriesDto>>();
        rows.Should().NotBeNull();
        rows!.Should().NotBeEmpty();
        rows!.Select(x => x.Name).Should().Contain("Indian Premier League");
    }

    [Fact]
    public async Task GetSeries_filters_by_seasonYear()
    {
        var resp = await _client.GetAsync("/api/v1/series?seasonYear=2026");
        resp.EnsureSuccessStatusCode();
        var rows = await resp.Content.ReadFromJsonAsync<IReadOnlyList<SeriesDto>>();
        rows.Should().NotBeNull();
        rows!.Should().OnlyContain(s => s.SeasonYear == 2026);
    }

    [Fact]
    public async Task GetSeriesById_returns_200_when_found_and_404_when_missing()
    {
        var existing = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2";
        var ok = await _client.GetAsync($"/api/v1/series/{existing}");
        ok.StatusCode.Should().Be(HttpStatusCode.OK);
        var dto = await ok.Content.ReadFromJsonAsync<SeriesDto>();
        dto.Should().NotBeNull();
        dto!.Id.ToString().Should().Be(existing);

        var missing = Guid.NewGuid();
        var notFound = await _client.GetAsync($"/api/v1/series/{missing}");
        notFound.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
