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
                Interval.OneSecond => State.OneSecond,
                Interval.FiveSeconds => State.FiveSeconds,
                Interval.OneMinute => State.OneMinute,
                Interval.HalfMinute => State.HalfMinute,
                Interval.OneHour => State.OneHour,
                Interval.HalfHour => State.HalfHour,
                Interval.OneDay => State.OneDaySaved,
                Interval.HalfDay => State.HalfDaySaved,
                Interval.OneWeek => State.OneWeekSaved,
                Interval.HalfWeek => State.HalfWeekSaved,
                _ => throw new NotImplementedException(),
            };

            var totalSavedSeconds = savedSeconds * frequency;
            var value = string.Empty;
            var nextInterval = Interval.OneWeek;
            float investment;
            string investmentType;

            while (value == string.Empty) {
                switch (nextInterval) {
                    case Interval.OneWeek:
                        nextInterval = Interval.OneDay;
                        investment = State.OneWeekDev;
                        investmentType = "developer";
                        break;

                    case Interval.OneDay:
                        nextInterval = Interval.OneHour;
                        investment = State.OneDayDev;
                        investmentType = "developer";
                        break;
                   
                    case Interval.OneHour:
                        nextInterval = Interval.OneMinute;
                        investment = State.OneHour;
                        investmentType = "hour";
                        break;

                    case Interval.OneMinute:
                        nextInterval = Interval.OneSecond;
                        investment = State.OneMinute;
                        investmentType = "minute";
                        break;

                    case Interval.OneSecond:
                        nextInterval = Interval.Undefined;
                        investment = State.OneSecond;
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
