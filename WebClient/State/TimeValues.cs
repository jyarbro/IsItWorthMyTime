using System;
using System.ComponentModel.DataAnnotations;

namespace WebClient.State {
    public class TimeValues {
        public event Action OnChange;

        [Range(1, 8760)]
        public int WorkYear {
            get => _WorkYear;
            set {
                if (_WorkYear != value) {
                    _WorkYear = value;
                    NotifyStateChanged();
                }
            }
        }
        int _WorkYear;
        int _WorkYearDefault = 2080;

        [Range(0, 8760)]
        public int AnnualPaidTimeOffHours {
            get => _AnnualPaidTimeOffHours;
            set {
                if (_AnnualPaidTimeOffHours != value) {
                    _AnnualPaidTimeOffHours = value;
                    NotifyStateChanged();
                }
            }
        }
        int _AnnualPaidTimeOffHours;
        int _AnnualPaidTimeOffHoursDefault = 80;

        [Range(0, 8760)]
        public int AnnualSickTimeOffHours {
            get => _AnnualSickTimeOffHours;
            set {
                if (_AnnualSickTimeOffHours != value) {
                    _AnnualSickTimeOffHours = value;
                    NotifyStateChanged();
                }
            }
        }
        int _AnnualSickTimeOffHours;
        int _AnnualSickTimeOffHoursDefault = 64;

        [Range(0, 8760)]
        public int AnnualHolidayTimeOffHours {
            get => _AnnualHolidayTimeOffHours;
            set {
                if (_AnnualHolidayTimeOffHours != value) {
                    _AnnualHolidayTimeOffHours = value;
                    NotifyStateChanged();
                }
            }
        }
        int _AnnualHolidayTimeOffHours;
        int _AnnualHolidayTimeOffHoursDefault = 68;

        [Range(0, 24)]
        public int DailyWorkHours {
            get => _DailyWorkHours;
            set {
                if (_DailyWorkHours != value) {
                    _DailyWorkHours = value;
                    NotifyStateChanged();
                }
            }
        }
        int _DailyWorkHours;
        int _DailyWorkHoursDefault = 8;

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        void NotifyStateChanged() {
            LastUpdate = DateTime.Now;
            OnChange?.Invoke();
        }

        public TimeValues() {
            Reset();
        }

        public void Reset() {
            WorkYear = _WorkYearDefault;
            AnnualPaidTimeOffHours = _AnnualPaidTimeOffHoursDefault;
            AnnualSickTimeOffHours = _AnnualSickTimeOffHoursDefault;
            AnnualHolidayTimeOffHours = _AnnualHolidayTimeOffHoursDefault;
            DailyWorkHours = _DailyWorkHoursDefault;
        }
    }
}
