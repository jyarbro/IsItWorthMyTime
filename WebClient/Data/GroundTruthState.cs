using System;

namespace WebClient.Data {
    public class GroundTruthState {
        public event Action OnChange;

        public int HoursPerYear {
            get => _HoursPerYear;
            set {
                if (_HoursPerYear != value) {
                    _HoursPerYear = value;
                    NotifyStateChanged();
                }
            }
        }
        int _HoursPerYear;
        readonly int HoursPerYearDefault = 2080;

        public int TimeOffHoursPerYear {
            get => _TimeOffHoursPerYear;
            set {
                if (_TimeOffHoursPerYear != value) {
                    _TimeOffHoursPerYear = value;
                    NotifyStateChanged();
                }
            }
        }
        int _TimeOffHoursPerYear;
        readonly int TimeOffHoursPerYearDefault = 262; // 80 federal holidays + 26 pay periods * (1 sick hour + 6 vacation hours)

        public int HoursPerDay {
            get => _HoursPerDay;
            set {
                if (_HoursPerDay != value) {
                    _HoursPerDay = value;
                    NotifyStateChanged();
                }
            }
        }
        int _HoursPerDay;
        readonly int HoursPerDayDefault = 8;

        public float DevHoursPerDay {
            get => _DevHoursPerDay;
            set {
                if (_DevHoursPerDay != value) {
                    _DevHoursPerDay = value;
                    NotifyStateChanged();
                }
            }
        }
        float _DevHoursPerDay;
        readonly float DevHoursPerDayDefault = 4;

        public int BreakMinutesPerDay {
            get => _BreakMinutesPerDay;
            set {
                if (_BreakMinutesPerDay != value) {
                    _BreakMinutesPerDay = value;
                    NotifyStateChanged();
                }
            }
        }
        int _BreakMinutesPerDay;
        readonly int BreakMinutesPerDayDefault = 30;

        public float RemainingWorkHours => HoursPerYear - TimeOffHoursPerYear;
        public float DaysPerYear => RemainingWorkHours / HoursPerDay;
        public float DaysPerMonth => DaysPerYear / 12;
        public float DaysPerWeek => DaysPerYear / 52;

        public float OneSecond = 1;
        public float FiveSeconds = 5;
        public float OneMinute = 60;
        public float HalfMinute = 30;
        public float OneHour = 3600;
        public float HalfHour = 1800;
        
        public float OneDaySaved => HoursPerDay * OneHour;
        public float HalfDaySaved => OneDaySaved / 2;
        public float OneWeekSaved => OneDaySaved * DaysPerWeek;
        public float HalfWeekSaved => OneWeekSaved / 2;

        public float OneDayDev => DevHoursPerDay * OneHour;
        public float OneWeekDev => OneDayDev * DaysPerWeek;

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public GroundTruthState() {
            Reset();
        }

        public void Reset() {
            HoursPerYear = HoursPerYearDefault;
            TimeOffHoursPerYear = TimeOffHoursPerYearDefault;
            HoursPerDay = HoursPerDayDefault;
            DevHoursPerDay = DevHoursPerDayDefault;
            BreakMinutesPerDay = BreakMinutesPerDayDefault;
        }

        void NotifyStateChanged() {
            LastUpdate = DateTime.Now;
            OnChange?.Invoke();
        }
    }
}
