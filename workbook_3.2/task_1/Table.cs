namespace BookingSystem
{
    public class Table
    {
        public int id { get; private set; }
        public string location { get; private set; }
        public int seats { get; private set; }
        public Dictionary<string, Booking> schedule;

        public Table(int id, string location, int seats)
        {
            if (id <= 0) throw new ArgumentException("ID стола должен быть положительным и отличным от нул€.");
            if (string.IsNullOrEmpty(location)) throw new ArgumentException("–асположение стола не может быть пустым.");
            if (seats <= 0) throw new ArgumentException(" оличество мест должно быть больше нул€.");

            this.id = id;
            this.location = location;
            this.seats = seats;
            this.schedule = new Dictionary<string, Booking>();

            initializeSchedule();
        }

        public void initializeSchedule()
        {
            for (int hour = 9; hour <= 18; hour++)
            {
                string slot = $"{hour}:00-{hour + 1}:00";
                schedule[slot] = null;
            }
        }

        public void updateTable(string newLocation, int newSeats)
        {
            if (hasActiveBookings()) throw new InvalidOperationException("Ќельз€ именить положение и количество мест стола с активными бронировани€ми.");
            if (string.IsNullOrEmpty(newLocation)) throw new ArgumentException("–асположение стола не может быть пустым.");
            if (newSeats <= 0) throw new ArgumentException(" оличество мест должно быть больше нул€.");

            location = newLocation;
            seats = newSeats;
        }

        public bool hasActiveBookings()
        {
            foreach (KeyValuePair<string, Booking> slot in schedule)
            {
                if (slot.Value == null) return true;
            }
            return false;
        }

        public void displayTableInfo()
        {
            Console.WriteLine($"ID: {id.ToString()}");
            Console.WriteLine($"–асположение: {location}");
            Console.WriteLine($" оличество мест: {seats}");
            foreach (KeyValuePair<string, Booking> slot in schedule)
            {
                string slotInfo = slot.Value == null ? "-" : $"ID {slot.Value.id}, {slot.Value.name}, {slot.Value.phone}";
                Console.WriteLine($"{slot.Key}: {slotInfo}");
            }
        }

        public void bookSlot(Booking booking, DateTime startBooking, DateTime endBooking)
        {
            TimeSpan duration = endBooking - startBooking;
            if (duration <= TimeSpan.Zero) throw new ArgumentException("¬рем€ начала брони должно быть раньше окончани€ брони.");

            int hours = (int)Math.Ceiling(duration.TotalHours);
            DateTime currentTime = startBooking;
            for (int i = 0; i < hours; i++)
            {
                string slot = $"{currentTime:HH}:00-{currentTime.Hour + 1}:00";
                if (!schedule.ContainsKey(slot)) throw new Exception($"—лот {slot} отсутствует в расписании.");
                if (schedule[slot] != null) throw new Exception($"—лот {slot} уже зан€т в расписании.");

                schedule[slot] = booking;
                currentTime = currentTime.AddHours(1);
            }
        }

        public void freeSlot(DateTime startBooking, DateTime endBooking)
        {
            DateTime currentTime = startBooking;
            while (currentTime < endBooking)
            {
                string slot = $"{currentTime:HH}:00-{currentTime.Hour + 1}:00";
                if (schedule.ContainsKey(slot))
                {
                    schedule[slot] = null;
                }
                currentTime = currentTime.AddHours(1);
            }
        }
    }
}
