using System;
using WebClient.Data;

namespace WebClient.Services {
    public class WorthCalculator {
        public GroundTruthState State { get; init; }

        public WorthCalculator(
            GroundTruthState state
        ) {
            State = state;
        }

        public string Calculate(Interval devInterval, Interval impactInterval, float frequency) {
            var impactSeconds = impactInterval switch {
                Interval.OneSecond => State.ImpactedPerson.OneSecond,
                Interval.FiveSeconds => State.ImpactedPerson.FiveSeconds,
                Interval.OneMinute => State.ImpactedPerson.OneMinute,
                Interval.HalfMinute => State.ImpactedPerson.HalfMinute,
                Interval.OneHour => State.ImpactedPerson.OneHour,
                Interval.HalfHour => State.ImpactedPerson.HalfHour,
                Interval.OneDay => State.ImpactedPerson.OneDay,
                Interval.HalfDay => State.ImpactedPerson.HalfDay,
                Interval.OneWeek => State.ImpactedPerson.OneWeek,
                Interval.HalfWeek => State.ImpactedPerson.HalfWeek,
                _ => throw new NotImplementedException(),
            };

            var devSeconds = devInterval switch {
                Interval.OneSecond => State.Developer.OneSecond,
                Interval.OneMinute => State.Developer.OneMinute,
                Interval.OneHour => State.Developer.OneHour,
                Interval.OneDay => State.Developer.OneDay,
                Interval.OneWeek => State.Developer.OneWeek,
                _ => throw new NotImplementedException(),
            };

            var totalImpact = impactSeconds * frequency;
            var totalDev = totalImpact / devSeconds;
            var value = Math.Floor(totalDev);

            return value > 0 ? value.ToString() : string.Empty;
        }
    }
}
