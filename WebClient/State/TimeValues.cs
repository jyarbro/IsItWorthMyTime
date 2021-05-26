using System;
using System.ComponentModel.DataAnnotations;

namespace WebClient.State {
    public class TimeValues {
        public event Action OnChange;

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        [Range(1, 8760)]
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
        readonly int _HoursPerYearDefault = 2080;

        [Range(0, 8760)]
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
        readonly int _TimeOffHoursPerYearDefault = 262; // 80 federal holidays + 26 pay periods * (1 sick hour + 6 vacation hours)

        [Range(0, 24)]
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
        readonly int _HoursPerDayDefault = 8;

        [Range(0, 24)]
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
        readonly int _BreakMinutesPerDayDefault = 30;

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
        public float OneDay => HoursPerDay * OneHour;
        public float HalfDay => OneDay / 2;
        public float OneWeek => OneDay * DaysPerWeek;

        public float WorkableSeconds => (RemainingWorkHours * OneHour) - (DaysPerYear * BreakMinutesPerDay * OneMinute);

        void NotifyStateChanged() {
            LastUpdate = DateTime.Now;
            OnChange?.Invoke();
        }

        public TimeValues() {
            Reset();
        }

        public void Reset() {
            HoursPerYear = _HoursPerYearDefault;
            TimeOffHoursPerYear = _TimeOffHoursPerYearDefault;
            HoursPerDay = _HoursPerDayDefault;
            BreakMinutesPerDay = _BreakMinutesPerDayDefault;
        }
    }
}
