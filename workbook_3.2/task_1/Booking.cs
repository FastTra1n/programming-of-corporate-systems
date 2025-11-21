using System.Numerics;
using System.Xml.Linq;

namespace BookingSystem
{
    public class Booking    
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public string phone { get; private set; }
        public DateTime startBooking { get; private set; }
        public DateTime endBooking { get; private set; }
        public string comment { get; private set; }
        public Table assignedTable { get; private set; }

        public Booking(int id, string name, string phone, DateTime startBooking, DateTime endBooking, string comment, Table assignedTable)
        {
            if (id <= 0) throw new ArgumentException("ID клиента должен быть положительным и отличным от нуля.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя не может быть пустым.");
            if (string.IsNullOrWhiteSpace(phone) || phone.Length != 11) throw new ArgumentException("Номер телефона неверный.");
            if (startBooking >= endBooking) throw new ArgumentException("Время начала брони должно быть раньше окончания брони.");
            if (assignedTable == null) throw new ArgumentNullException("Стол не был найден.");

            this.id = id;
            this.name = name;
            this.phone = phone;
            this.startBooking = startBooking;
            this.endBooking = endBooking;
            this.comment = comment;
            this.assignedTable = assignedTable;

            assignedTable.bookSlot(this, startBooking, endBooking);
        }

        public void updateBooking(string newName, string newPhone, DateTime newStartBooking, DateTime newEndBooking, string newComment, Table newAssignedTable)
        {
            if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Имя не может быть пустым.");
            if (string.IsNullOrWhiteSpace(newPhone) || phone.Length != 11) throw new ArgumentException("Номер телефона неверный.");
            if (newStartBooking >= newEndBooking) throw new ArgumentException("Время начала брони должно быть раньше окончания брони.");
            if (newAssignedTable == null) throw new ArgumentNullException("Стол не был найден.");

            assignedTable.freeSlot(startBooking, endBooking);

            name = newName;
            phone = newPhone;
            startBooking = newStartBooking;
            endBooking = newEndBooking;
            comment = newComment;
            assignedTable = newAssignedTable;

            if (newAssignedTable != null && newAssignedTable != assignedTable)
            {
                assignedTable = newAssignedTable;
            }

            assignedTable.bookSlot(this, newStartBooking, newEndBooking);
        }

        public void cancelBooking()
        {
            assignedTable.freeSlot(startBooking, endBooking);
        }

        public void displayBookingInfo()
        {
            Console.WriteLine($"ID клиента: {id}");
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Телефон: {phone}");
            Console.WriteLine($"Время: {startBooking:HH:mm} - {endBooking:HH:mm}");
            Console.WriteLine($"Комментарий: {comment}");
            Console.WriteLine($"Стол: ID {assignedTable.id}");
        }
    }
}