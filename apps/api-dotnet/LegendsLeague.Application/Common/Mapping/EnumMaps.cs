using System;
using C = LegendsLeague.Contracts.Fantasy;
using D = LegendsLeague.Domain.Entities.Fantasy;

namespace LegendsLeague.Application.Common.Mapping;

/// <summary>
/// Helpers to convert between Contracts and Domain fantasy enums.
/// Uses name-based conversion to avoid brittle numeric casts.
/// </summary>
public static class EnumMaps
{
    public static D.DraftType ToDomain(this C.DraftType v) =>
        Enum.Parse<D.DraftType>(v.ToString());

    public static D.DraftStatus ToDomain(this C.DraftStatus v) =>
        Enum.Parse<D.DraftStatus>(v.ToString());

    public static D.DraftPickStatus ToDomain(this C.DraftPickStatus v) =>
        Enum.Parse<D.DraftPickStatus>(v.ToString());

    public static D.LeagueMemberRole ToDomain(this C.LeagueMemberRole v) =>
        Enum.Parse<D.LeagueMemberRole>(v.ToString());

    public static D.LeagueMemberStatus ToDomain(this C.LeagueMemberStatus v) =>
        Enum.Parse<D.LeagueMemberStatus>(v.ToString());

    public static D.RosterStatus ToDomain(this C.RosterStatus v) =>
        Enum.Parse<D.RosterStatus>(v.ToString());

    public static D.RosterSlot ToDomain(this C.RosterSlot v) =>
        Enum.Parse<D.RosterSlot>(v.ToString());

    // Reverse direction (Domain -> Contracts), if needed
    public static C.DraftStatus ToContract(this D.DraftStatus v) =>
        Enum.Parse<C.DraftStatus>(v.ToString());

    public static C.DraftPickStatus ToContract(this D.DraftPickStatus v) =>
        Enum.Parse<C.DraftPickStatus>(v.ToString());
}
