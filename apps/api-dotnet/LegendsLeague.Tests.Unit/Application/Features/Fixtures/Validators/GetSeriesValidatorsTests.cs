using FluentAssertions;
using FluentValidation.TestHelper;
using LegendsLeague.Application.Features.Fixtures.Series.Queries;
using LegendsLeague.Application.Features.Fixtures.Series.Queries.Validators;

namespace LegendsLeague.Tests.Unit.Application.Features.Fixtures.Validators;

/// <summary>
/// Unit tests for query validators in the Fixtures read path.
/// </summary>
public class GetSeriesValidatorsTests
{
    [Fact]
    public void ListQuery_rejects_bad_paging()
    {
        var v = new GetSeriesListQueryValidator();
        v.TestValidate(new GetSeriesListQuery(Page: 0)).ShouldHaveValidationErrorFor(x => x.Page);
        v.TestValidate(new GetSeriesListQuery(PageSize: 0)).ShouldHaveValidationErrorFor(x => x.PageSize);
        v.TestValidate(new GetSeriesListQuery(PageSize: 101)).ShouldHaveValidationErrorFor(x => x.PageSize);
    }

    [Fact]
    public void ListQuery_accepts_valid_sort_keys()
    {
        var v = new GetSeriesListQueryValidator();
        v.TestValidate(new GetSeriesListQuery(Sort: "name")).IsValid.Should().BeTrue();
        v.TestValidate(new GetSeriesListQuery(Sort: "-seasonYear")).IsValid.Should().BeTrue();
        v.TestValidate(new GetSeriesListQuery(Sort: "x")).IsValid.Should().BeFalse();
    }

    [Fact]
    public void ByIdQuery_requires_non_empty_id()
    {
        var v = new GetSeriesByIdQueryValidator();
        v.TestValidate(new GetSeriesByIdQuery(Guid.Empty)).ShouldHaveValidationErrorFor(x => x.Id);
        v.TestValidate(new GetSeriesByIdQuery(Guid.NewGuid())).IsValid.Should().BeTrue();
    }
}
