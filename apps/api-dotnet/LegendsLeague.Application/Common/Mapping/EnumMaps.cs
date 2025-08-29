using System;
using C = LegendsLeague.Contracts.Fantasy;
using D = LegendsLeague.Domain.Entities.Fantasy;

namespace LegendsLeague.Application.Common.Mapping;

/// <summary>
/// Helpers to convert between Contracts and Domain fantasy enums.
/// We rely on name-based conversion to preserve numeric values.
/// </summary>
public static class EnumMaps
{
    // League members
    public static D.LeagueMemberRole   ToDomain(this C.LeagueMemberRole v)   => Enum.Parse<D.LeagueMemberRole>(v.ToString());
    public static D.LeagueMemberStatus ToDomain(this C.LeagueMemberStatus v) => Enum.Parse<D.LeagueMemberStatus>(v.ToString());
    public static C.LeagueMemberRole   ToContract(this D.LeagueMemberRole v) => Enum.Parse<C.LeagueMemberRole>(v.ToString());
    public static C.LeagueMemberStatus ToContract(this D.LeagueMemberStatus v) => Enum.Parse<C.LeagueMemberStatus>(v.ToString());

    // Drafts
    public static D.DraftType    ToDomain(this C.DraftType v)    => Enum.Parse<D.DraftType>(v.ToString());
    public static D.DraftStatus  ToDomain(this C.DraftStatus v)  => Enum.Parse<D.DraftStatus>(v.ToString());
    public static D.DraftPickStatus ToDomain(this C.DraftPickStatus v) => Enum.Parse<D.DraftPickStatus>(v.ToString());
    public static C.DraftType    ToContract(this D.DraftType v)    => Enum.Parse<C.DraftType>(v.ToString());
    public static C.DraftStatus  ToContract(this D.DraftStatus v)  => Enum.Parse<C.DraftStatus>(v.ToString());
    public static C.DraftPickStatus ToContract(this D.DraftPickStatus v) => Enum.Parse<C.DraftPickStatus>(v.ToString());

    // Scoring
    public static D.ScoringType  ToDomain(this C.ScoringType v)  => Enum.Parse<D.ScoringType>(v.ToString());
    public static C.ScoringType  ToContract(this D.ScoringType v) => Enum.Parse<C.ScoringType>(v.ToString());

    // Rosters
    public static D.RosterSlot   ToDomain(this C.RosterSlot v)   => Enum.Parse<D.RosterSlot>(v.ToString());
    public static D.RosterStatus ToDomain(this C.RosterStatus v) => Enum.Parse<D.RosterStatus>(v.ToString());
    public static C.RosterSlot   ToContract(this D.RosterSlot v)   => Enum.Parse<C.RosterSlot>(v.ToString());
    public static C.RosterStatus ToContract(this D.RosterStatus v) => Enum.Parse<C.RosterStatus>(v.ToString());
}
