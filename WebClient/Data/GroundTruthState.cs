using System;

namespace WebClient.Data {
    public class GroundTruthState {
        public event Action OnChange;

        public TimeValues ImpactedPerson { get; init; }
        public TimeValues Developer { get; init; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public GroundTruthState() {
            ImpactedPerson = new();
            ImpactedPerson.OnChange += NotifyStateChanged;

            Developer = new();
            Developer.OnChange += NotifyStateChanged;
        }

        void NotifyStateChanged() {
            LastUpdate = DateTime.Now;
            OnChange?.Invoke();
        }
    }
}
