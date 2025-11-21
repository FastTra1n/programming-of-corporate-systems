using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace BookingSystem
{
    internal class Program
    {
        private static int tableId = 1;
        private static int clientId = 1;
        private static List<Table> tables = new List<Table>();
        private static List<Booking> bookings = new List<Booking>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Добро пожаловать в систему бронирования!\nМеню:");
                Console.WriteLine("1. Создать стол");
                Console.WriteLine("2. Редактировать стол");
                Console.WriteLine("3. Вывести информацию о столе");
                Console.WriteLine("4. Создать бронирование");
                Console.WriteLine("5. Редактировать бронирование");
                Console.WriteLine("6. Отменить бронирование");
                Console.WriteLine("7. Вывести доступные столы");
                Console.WriteLine("8. Вывести все бронирования");
                Console.WriteLine("9. Поиск брони по телефону и имени");
                Console.WriteLine("10. Выход");

                Console.Write("Ваш выбор: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Неверный ввод: перепроверьте корректность введённого числа.");
                    continue;
                }
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Выбран пункт №1.");
                            createTable();
                            break;

                        case 2:
                            Console.WriteLine("Выбран пункт №2.");
                            updateTable();
                            break;

                        case 3:
                            Console.WriteLine("Выбран пункт №3.");
                            displayTableInfo();
                            break;

                        case 4:
                            Console.WriteLine("Выбран пункт №4.");
                            createBooking();
                            break;

                        case 5:
                            Console.WriteLine("Выбран пункт №5.");
                            updateBooking();
                            break;

                        case 6:
                            Console.WriteLine("Выбран пункт №6.");
                            cancelBooking();
                            break;

                        case 7:
                            Console.WriteLine("Выбран пункт №7.");
                            showAvailableTables();
                            break;

                        case 8:
                            Console.WriteLine("Выбран пункт №8.");
                            showAvailableBookings();
                            break;

                        case 9:
                            Console.WriteLine("Выбран пункт №9.");
                            searchBooking();
                            break;

                        case 10:
                            Console.WriteLine("Выбран пункт №10.");
                            Console.WriteLine("Завершение работы...");
                            return;

                        default:
                            Console.WriteLine("Неверный выбор: проверьте, что введённое число принадлежит диапазону от 1 до 10 включительно.");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"При выполнении программы возникла ошибка: {e.Message}");
                }
            }
        }

        private static void createTable()
        {
            string location;

            Console.WriteLine($"Создание столика №{tableId}.");
            Console.Write("Введите расположение столика: ");
            location = Console.ReadLine() ?? "Не задано";
            Console.Write("Введите количество мест за столиком: ");
            if (!int.TryParse(Console.ReadLine(), out int seats) || seats <= 0) throw new ArgumentException("Введено неверное количество мест. Введите целое положительное число мест, отличное от нуля.");

            Table table = new Table(tableId++, location, seats);
            tables.Add(table);
            Console.WriteLine("Столик был успешно создан.");
        }
        private static void updateTable()
        {
            string newLocation;

            Console.Write("Введите уникальный идентификатор столика (ID): ");
            if (!int.TryParse(Console.ReadLine(), out int id)) throw new ArgumentException("Введён неверный ID. Проверьте корректность введённого идентификатора.");

            Table table = tables.SingleOrDefault(t => t.id == id) ?? throw new KeyNotFoundException("Стол с введённым идентификатором не найден. Убедитесь в том, что данный столик существует.");
            Console.Write("Стол найден. Введите новое расположение стола: ");
            newLocation = Console.ReadLine() ?? "";
            Console.Write("Введите новое количество мест за столом: ");
            if (!int.TryParse(Console.ReadLine(), out int newSeats) || newSeats <= 0) throw new ArgumentException("Введено неверное количество мест. Введите целое положительное число мест, отличное от нуля.");
            table.updateTable(newLocation, newSeats);
            Console.WriteLine("Стол был успешно обновлён.");
        }

        public static void displayTableInfo()
        {
            Console.Write("Введите уникальный идентификатор столика (ID): ");
            if (!int.TryParse(Console.ReadLine(), out int id)) throw new ArgumentException("Введён неверный ID. Проверьте корректность введённого идентификатора.");

            Table table = tables.SingleOrDefault(t => t.id == id) ?? throw new KeyNotFoundException("Стол с введённым идентификатором не найден. Убедитесь в том, что данный столик существует.");
            Console.WriteLine("Стол найден. Информация о столике:");
            table.displayTableInfo();
        }

        public static void createBooking()
        {
            string name, phone, comment;

            Console.WriteLine($"Создание брони для клиента №{clientId}");
            Console.Write("Введите имя клиента: ");
            name = Console.ReadLine() ?? "";
            Console.Write("Введите телефон клиента: ");
            phone = Console.ReadLine() ?? "";
            Console.Write("Введите время начала брони (HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startBooking)) throw new ArgumentException("Введено неверное время начала брони. Введите время начала брони в соответствии с указанным шаблоном.");
            Console.Write("Введите время окончания брони (HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endBooking)) throw new ArgumentException("Введено неверное время окончания брони. Введите время окончания брони в соответствии с указанным шаблоном.");
            Console.Write("Введите комментарий к брони: ");
            comment = Console.ReadLine() ?? "";
            Console.Write("Введите ID стола: ");
            if (!int.TryParse(Console.ReadLine(), out int tableId)) throw new ArgumentException("Введён неверный ID. Проверьте корректность введённого идентификатора.");

            Table table = tables.SingleOrDefault(t => t.id == tableId) ?? throw new KeyNotFoundException("Стол с введённым идентификатором не найден. Убедитесь в том, что данный столик существует.");
            
            DateTime currentTime = startBooking;
            while (currentTime < endBooking)
            {
                string slot = $"{currentTime:HH}:00-{currentTime.Hour + 1}:00";
                if (table.schedule.ContainsKey(slot) && table.schedule[slot] != null)
                {
                    throw new Exception($"Столик уже забронирован на время {slot}.");
                }
                currentTime = currentTime.AddHours(1);
            }
            Booking booking = new Booking(clientId++, name, phone, startBooking, endBooking, comment, table);
            bookings.Add(booking);
            Console.WriteLine("Бронирование было успешно создано.");
        }

        public static void updateBooking()
        {
            string newName, newPhone, newComment;

            Console.Write("Введите уникальный идентификатор клиента (ID): ");
            if (!int.TryParse(Console.ReadLine(), out int id)) throw new ArgumentException("Введён неверный ID. Проверьте корректность введённого идентификатора.");

            Booking booking = bookings.SingleOrDefault(b => b.id == id) ?? throw new KeyNotFoundException("Бронирование с введённым идентификатором клиента не найдена. Убедитесь в том, что данный клиент или бронь существует.");

            Console.Write("Бронирование найдено. Введите новое имя клиента: ");
            newName = Console.ReadLine() ?? booking.name;
            Console.Write("Введите новый номер телефон: ");
            newPhone = Console.ReadLine() ?? booking.phone;
            Console.Write("Введите новое время начала брони (HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newStartBooking)) throw new ArgumentException("Введено неверное время начала брони. Введите время начала брони в соответствии с указанным шаблоном.");
            Console.Write("Введите новое время окончания брони (HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newEndBooking)) throw new ArgumentException("Введено неверное время окончания брони. Введите время окончания брони в соответствии с указанным шаблоном.");
            Console.Write("Введите новый комментарий к брони: ");
            newComment = Console.ReadLine() ?? booking.comment;
            Console.Write("Введите новый ID стола: ");
            if (!int.TryParse(Console.ReadLine(), out int newTableId)) throw new ArgumentException("Введён неверный ID. Проверьте корректность введённого идентификатора.");

            Table newTable = tables.SingleOrDefault(t => t.id == newTableId) ?? throw new KeyNotFoundException("Стол с введённым идентификатором не найден. Убедитесь в том, что данный столик существует.");

            
            booking.updateBooking(newName, newPhone, newStartBooking, newEndBooking, newComment, newTable);
            Console.WriteLine("Бронирование было успешно обновлено.");
        }

        public static void cancelBooking()
        {
            Console.Write("Введите уникальный идентификатор клиента (ID): ");
            if (!int.TryParse(Console.ReadLine(), out int id)) throw new ArgumentException("Введён неверный ID. Проверьте корректность введённого идентификатора.");

            Booking booking = bookings.SingleOrDefault(b => b.id == id) ?? throw new KeyNotFoundException("Бронирование с введённым идентификатором клиента не найдена. Убедитесь в том, что данный клиент или бронь существует.");
            booking.cancelBooking();
            bookings.Remove(booking);
            Console.WriteLine("Бронирование было успешно отменено.");
        }

        public static void showAvailableTables()
        {
            string location;

            Console.WriteLine("Для просмотра доступных столиков, укажите следующие параметры поиска:");
            Console.Write("Введите расположение столика: ");
            location = Console.ReadLine() ?? "";
            Console.Write("Введите минимальное количество мест: ");
            if (!int.TryParse(Console.ReadLine(), out int seats) || seats <= 0) throw new ArgumentException("Введено неверное количество мест. Введите целое положительное число мест, отличное от нуля.");
            Console.Write("Введите время начала (HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startBooking)) throw new ArgumentException("Введено неверное время начала. Введите время начала в соответствии с указанным шаблоном.");
            Console.Write("Введите время окончания (HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endBooking)) throw new ArgumentException("Введено неверное время окончания. Введите время окончания в соответствии с указанным шаблоном.");

            List<Table> filteredTables = tables.FindAll(t => 
                (t.location == location) &&
                (t.seats >= seats) &&
                isAvailableInTime(t, startBooking, endBooking)
            );

            if (filteredTables == null)
            {
                Console.WriteLine("Столики по указанным критериям не найдены.");
                return;
            }

            Console.WriteLine($"Найдено {filteredTables.Count} доступных столиков.");
            foreach (Table table in filteredTables)
            {
                table.displayTableInfo();
                Console.WriteLine("\n");
            }
        }

        private static bool isAvailableInTime(Table table, DateTime startBooking, DateTime endBooking)
        {
            DateTime current = startBooking;
            while (current < endBooking)
            {
                string slot = $"{current.Hour:00}:00-{(current.Hour + 1):00}:00";
                if (table.schedule.ContainsKey(slot) && table.schedule[slot] != null) return false;
                current = current.AddHours(1);
            }
            return true;
        }

        public static void showAvailableBookings()
        {
            Console.WriteLine("Список бронирований:");
            foreach (Booking booking in bookings)
            {
                booking.displayBookingInfo();
                Console.WriteLine("\n");
            }
        }

        public static void searchBooking()
        {
            string name, phone;

            Console.WriteLine("Для нахождения бронирования, укажите следующие данные:");
            Console.Write("Имя клиента: ");
            name = Console.ReadLine() ?? "";
            Console.Write("Телефон клиента (последние 4 цифры): ");
            phone = Console.ReadLine() ?? "";
            if (phone.Length != 4) throw new ArgumentException("Введите последние 4 цифры телефона клиента.");

            Booking? filteredBooking = bookings.Find(b => b.name == name && b.phone.EndsWith(phone));

            if (filteredBooking == null)
            {
                Console.WriteLine("Бронирование с указанным именем и номером не найдено.");
            }
            else
            {
                Console.WriteLine($"Найдено бронирование на имя {filteredBooking.name}:");
                filteredBooking.displayBookingInfo();
            }
        }
    }
}
