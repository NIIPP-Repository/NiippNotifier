namespace NIIPP.DatabaseClient.DataStorage
{
    /// <summary>
    /// Класс содержит данные для подключения SQL службы к серверу
    /// </summary>
    public sealed class ConnectionSettings
    {
        public static string
            ServerIp = "172.16.1.24", // IP адрес сервера
            UserId = "user4", // название пользователя SQL 
            Password = "user4pass", // пароль пользователя SQL
            DatabaseName = "db_wafers"; // название базы данных
    }

    /// <summary>
    /// Класс содержит сведения относительно МАС-адресов сотрудников 41 лаборатории ОАО "НИИПП"
    /// </summary>
    public static class TbPeopleHardwareAddress
    {
        /// <summary>
        /// Название таблицы
        /// </summary>
        public static string Name = "tb_people_hardware_address";

        /// <summary>
        /// Фамилия и инициалы сотрудника (пример: Петров И. В.)
        /// </summary>
        public static string SurnameAndName = "name";

        /// <summary>
        /// Последовательность всех MAC-адресов конкретного сотрудника
        /// </summary>
        public static string MacAddress = "mac_address";
    }

    /// <summary>
    /// Класс содержит сведения относительно таблицы "tb_inter_control",
    /// которая хранит информацию о промежуточном контроле пластин
    /// </summary>
    public static class TbInterControl
    {
        private const string _name = "tb_inter_control";
        /// <summary>
        /// Название таблицы
        /// </summary>
        public static string Name
        {
            get { return _name; }
        }
        private const string _numberOfSheet = "number_of_sheet";
        /// <summary>
        /// Идентификатор записи - номер сопроводительного листа / год
        /// </summary>
        public static string NumberOfSheet
        {
            get { return _numberOfSheet; }
        }
        private const string _lastUpdate = "last_update";
        /// <summary>
        /// Дата и время последнего изменения записи
        /// </summary>
        public static string LastUpdate
        {
            get { return _lastUpdate; }
        }
        private const string _author = "author";
        /// <summary>
        /// Автор последнего изменения
        /// </summary>
        public static string Author
        {
            get { return _author; }
        }
        private const string _sheetDate = "sheet_date";
        /// <summary>
        /// Дата создания сопроводного листа
        /// </summary>
        public static string SheetDate
        {
            get { return _sheetDate; }
        }
        private const string _nameOfScheme = "name_of_scheme";
        /// <summary>
        /// Схема фотошаблона
        /// </summary>
        public static string NameOfScheme
        {
            get { return _nameOfScheme; }
        }
        private const string _cell = "cell";
        /// <summary>
        /// Номер ячейки в которой лежит пластина
        /// </summary>
        public static string Cell
        {
            get { return _cell; }
        }
        private const string _elDateOfIssue = "el_date_of_issue";
        /// <summary>
        /// Дата выдачи номер 1
        /// </summary>
        public static string ElementDateOfIssue
        {
            get { return _elDateOfIssue; }
        }
        private const string _startOfMeas = "start_of_meas";
        /// <summary>
        /// Дата начала межоперационных измерений
        /// </summary>
        public static string StartOfMeasurement
        {
            get { return _startOfMeas; }
        }
        private const string _devDateOfIssue = "dev_date_of_issue";
        /// <summary>
        /// Дата выдачи номер 2
        /// </summary>
        public static string DeviceDateOfIssue
        {
            get { return _devDateOfIssue; }
        }
        private const string _meas1 = "meas1";
        /// <summary>
        /// Дата измерения номер 1
        /// </summary>
        public static string Meas1
        {
            get { return _meas1; }
        }
        private const string _meas2 = "meas2";
        /// <summary>
        /// Дата измерения номер 2
        /// </summary>
        public static string Meas2
        {
            get { return _meas2; }
        }
        private const string _procentOfGood = "procent_of_good";
        /// <summary>
        /// Процент выхода годных 0..100 % (тип 'double')
        /// </summary>
        public static string ProcentOfGood
        {
            get { return _procentOfGood; }
        }
        private const string _countOfGood = "count_of_good";
        /// <summary>
        /// Количество годных чипов (тип 'int')
        /// </summary>
        public static string CountOfGood
        {
            get { return _countOfGood; }
        }
        private const string _note = "note";
        /// <summary>
        /// Комментарий (тип 'varchar 16255')
        /// </summary>
        public static string Note
        {
            get { return _note; }
        }
        private const string _completed = "completed";
        /// <summary>
        /// Завершена ли работа с этой пластиной 'NO' либо 'YES' (тип 'varchar 255')
        /// </summary>
        public static string IsCompleted
        {
            get { return _completed; }
        }
    }
}
