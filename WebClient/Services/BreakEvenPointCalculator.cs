using System;
using WebClient.Data;

namespace WebClient.Services {
    public class BreakEvenPointCalculator {
        public GroundTruthState State { get; init; }

        public BreakEvenPointCalculator(
            GroundTruthState state
        ) {
            State = state;
        }

        public string Calculate(Interval timeSaved, float frequency) {
            var savedSeconds = timeSaved switch {
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

            var totalSavedSeconds = savedSeconds * frequency;
            var value = string.Empty;
            var nextInterval = Interval.OneDay;
            float investment;
            string investmentType;

            while (value == string.Empty) {
                switch (nextInterval) {
                    case Interval.OneDay:
                        nextInterval = Interval.OneHour;
                        investment = State.Developer.OneDay;
                        investmentType = "developer";
                        break;
                   
                    case Interval.OneHour:
                        nextInterval = Interval.OneMinute;
                        investment = State.Developer.OneHour;
                        investmentType = "hour";
                        break;

                    case Interval.OneMinute:
                        nextInterval = Interval.OneSecond;
                        investment = State.Developer.OneMinute;
                        investmentType = "minute";
                        break;

                    case Interval.OneSecond:
                        nextInterval = Interval.Undefined;
                        investment = State.Developer.OneSecond;
                        investmentType = "second";
                        break;

                    default:
                    case Interval.Undefined:
                        return "";
                };

                var totalDev = Math.Floor(totalSavedSeconds / investment);

                if (totalDev > 0) {
                    value = $"{totalDev}<br />{investmentType}{(totalDev == 1 ? "" : "s")}";
                }
            }

            return value;
        }
    }
}
