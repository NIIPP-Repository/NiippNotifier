﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NIIPP.DatabaseClient.DataStorage;

namespace NIIPP.DatabaseClient.Library
{
    /// <summary>
    /// Класс - объектное представление SQL таблицы
    /// </summary>
    public class SqlTable
    {
        /// <summary>
        /// Название таблицы текущего объекта
        /// </summary>
        private readonly string _nameOfTable;

        /// <summary>
        /// Существует ли данная таблица
        /// </summary>
        public bool Exist
        {
            get
            {
                string query = String.Format("SHOW TABLES LIKE '{0}'", _nameOfTable);
                DataTable dt = SqlQuery.Execute(query);
                return dt.Rows.Count > 0;
            }
        }

        /// <summary>
        /// Связывает данный объект с SQL таблицей
        /// </summary>
        /// <param name="nameOfTable">Имя таблицы</param>
        public SqlTable(string nameOfTable)
        {
            _nameOfTable = nameOfTable;
        }

        /// <summary>
        /// Создает текущую таблицу на SQL сервере
        /// </summary>
        /// <param name="nameOfIdField">Название столбца являющегося уникальным ключом</param>
        /// <param name="typeOfIdField">Тип данных уникального ключа</param>
        public bool Create(string nameOfIdField, Type typeOfIdField)
        {
            string query = String.Format("CREATE TABLE `{0}` (`{1}` {2} NOT NULL PRIMARY KEY);", _nameOfTable,
                nameOfIdField, SqlType.GetTypeInSqlFormat(typeOfIdField));
            return SqlQuery.TryExecute(query);
        }

        /// <summary>
        /// Создает копию указанной таблицы на SQL сервере (данный объект будет связан с копией)
        /// </summary>
        /// <param name="nameOfOriginalTable">Название копируемой таблицы</param>
        public void CreateCopy(string nameOfOriginalTable)
        {
            string query1 = String.Format("CREATE TABLE `{0}` LIKE `{1}`;", _nameOfTable, nameOfOriginalTable);
            SqlQuery.TryExecute(query1);
            string query2 = String.Format("INSERT `{0}` SELECT * FROM `{1}`;", _nameOfTable, nameOfOriginalTable);
            SqlQuery.TryExecute(query2);
        }

        /// <summary>
        /// Удаляет текущую таблицу с SQL сервера
        /// </summary>
        public void Remove()
        {
            string query = String.Format("DROP TABLE `{0}`", _nameOfTable);
            SqlQuery.TryExecute(query);
        }

        /// <summary>
        /// Возвращает объект отдельной записи в данной таблице
        /// </summary>
        /// <param name="nameOfIdField">Название столбца - уникального ключа таблицы</param>
        /// <param name="valueOfIdField">Значение уникального ключа требуемой записи</param>
        /// <returns></returns>
        public SqlRecord GetRecord(string nameOfIdField, object valueOfIdField)
        {
            return new SqlRecord(_nameOfTable, nameOfIdField, valueOfIdField);
        }

        /// <summary>
        /// Возвращает объект для выборки данных из данной таблицы
        /// </summary>
        /// <returns></returns>
        public SqlSelect GetSelect()
        {
            return new SqlSelect(_nameOfTable);
        }
    }

    /// <summary>
    /// Класс для отправки SQL запросов
    /// </summary>
    public static class SqlQuery
    {
        /// <summary>
        /// Отправлет SQL запрос и получает данные
        /// </summary>
        /// <param name="strQuery">Строка SQL запроса</param>
        /// <returns>Данные от SQL запроса</returns>
        public static DataTable Execute(String strQuery)
        {
            MySqlConnectionStringBuilder mysqlCsb = new MySqlConnectionStringBuilder
            {
                Server = ConnectionSettings.ServerIp,
                Database = ConnectionSettings.DatabaseName,
                UserID = ConnectionSettings.UserId,
                Password = ConnectionSettings.Password
            };

            MySqlConnection connection = new MySqlConnection { ConnectionString = mysqlCsb.ConnectionString };

            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand { Connection = connection, CommandText = strQuery };
                MySqlDataReader sqlDataReader = command.ExecuteReader();

                if (sqlDataReader.HasRows)
                    dataTable.Load(sqlDataReader);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось выполнить SQL запрос \n" + exc.Message);
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }

        /// <summary>
        /// Отправлет SQL запрос и получает состояние запроса
        /// </summary>
        /// <param name="strQuery">Строка SQL запроса</param>
        /// <returns>Состояние запроса (успешный запрос - true, произошла ошибка - false)</returns>
        public static bool TryExecute(String strQuery)
        {
            MySqlConnectionStringBuilder mysqlCsb = new MySqlConnectionStringBuilder
            {
                Server = ConnectionSettings.ServerIp,
                Database = ConnectionSettings.DatabaseName,
                UserID = ConnectionSettings.UserId,
                Password = ConnectionSettings.Password
            };
            MySqlConnection connection = new MySqlConnection { ConnectionString = mysqlCsb.ConnectionString };

            bool res = true;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand { Connection = connection, CommandText = strQuery };
                command.ExecuteReader();
            }
            catch
            {
                res = false;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }
    }

    /// <summary>
    /// Класс содержит типы данных SQL полей
    /// </summary>
    public static class SqlType
    {
        /// <summary>
        /// Формат данных для строк не превышающих 255 символов
        /// </summary>
        public static readonly Type StringFormat = typeof(string);

        /// <summary>
        /// Формат данных для чисел с плавающей запятой
        /// </summary>
        public static readonly Type DoubleFormat = typeof(double);

        /// <summary>
        /// Формат данных для целых чисел не превышающих по модулю 2 147 483 647
        /// </summary>
        public static readonly Type IntFormat = typeof(int);

        /// <summary>
        /// Формат данных для текста не превышающего 16000 символов
        /// </summary>
        public static readonly Type TextFormat = typeof(string[]);

        /// <summary>
        /// Формат данных для даты и времени
        /// </summary>
        public static readonly Type DateTimeFormat = typeof(DateTime);

        /// <summary>
        /// Возвращает данные в формате SQL
        /// </summary>
        /// <param name="obj">Объект, который необходимо преобразовать в SQL совместимую строку</param>
        /// <returns>SQL совместимая строка</returns>
        public static string GetStringInSqlFormat(object obj)
        {
            Type type = obj.GetType();

            string res = "NULL";
            if (type == typeof(string))
                res = (string)obj;
            if (type == typeof(int))
                res = ((int)obj).ToString();
            if (type == typeof(double))
                res = ((double)obj).ToString(CultureInfo.InvariantCulture);
            if (type == typeof(DateTime))
                res = ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss");

            if (res != "NULL")
                return String.Format("'{0}'", res);
            else
                return res;
        }

        /// <summary>
        /// Возвращает тип в SQL формате
        /// </summary>
        /// <param name="type">Тип в формате .NET</param>
        /// <returns>Строка описывающая тип в SQL формате</returns>
        public static string GetTypeInSqlFormat(Type type)
        {
            string res = "";
            if (type == typeof(string))
                res = "varchar(255)";
            if (type == typeof(string[]))
                res = "varchar(16000)";
            if (type == typeof(int))
                res = "int(11)";
            if (type == typeof(double))
                res = "double";
            if (type == typeof(DateTime))
                res = "datetime";

            return res;
        }
    }

    /// <summary>
    /// Класс - объектное представление записи в таблице
    /// </summary>
    public class SqlRecord
    {
        /// <summary>
        /// Названиетаблицы
        /// </summary>
        private readonly string _nameOfTable;
        /// <summary>
        /// Название столбца - уникального ключа таблицы
        /// </summary>
        private readonly string _nameOfIdField;
        /// <summary>
        /// Значение уникального ключа данной записи
        /// </summary>
        private readonly object _valueOfIdField;
        /// <summary>
        /// Массив пар (название столбца - значение поля)
        /// </summary>
        private readonly Dictionary<string, Object> _setOfNameValue = new Dictionary<string, Object>();

        /// <summary>
        /// Задает режим, true - если используемые столбцы не существуют, то они будут созданы;
        /// false - если используемые столбцы не существуют, произойдет ошибка;
        /// </summary>
        public bool CreateFieldsIfNotExist { get; set; }

        /// <summary>
        /// Существует ли данная запись в таблице, true - да, false - нет
        /// </summary>
        public bool Exist
        {
            get
            {
                string query = String.Format("SELECT `{0}` FROM `{1}` WHERE `{2}` = {3}", _nameOfIdField, _nameOfTable, _nameOfIdField, SqlType.GetStringInSqlFormat(_valueOfIdField));
                DataTable dt = SqlQuery.Execute(query);
                return dt.Rows.Count > 0;
            }
        }

        /// <summary>
        /// Создает новый объект - запись в таблице
        /// </summary>
        /// <param name="nameOfTable">Название таблицы</param>
        /// <param name="nameOfIdField">Название столбца - уникального ключа таблицы</param>
        /// <param name="valueOfIdField">Значение уникального ключа</param>
        public SqlRecord(string nameOfTable, string nameOfIdField, object valueOfIdField)
        {
            // установка параметров по умолчанию
            CreateFieldsIfNotExist = false;

            _nameOfTable = nameOfTable;
            _nameOfIdField = nameOfIdField;
            _valueOfIdField = valueOfIdField;
            _setOfNameValue.Clear();
        }

        /// <summary>
        /// Производит обновление записи в таблице использую пользовательские установки
        /// (при условии что уникальный ключ записи уже сохранен в данной записи)
        /// </summary>
        private void Update()
        {
            if (_setOfNameValue.Count == 0)
                return;

            string query = String.Format("UPDATE `{0}` SET ", _nameOfTable);
            query = _setOfNameValue.Aggregate(query, (current, next) => current + String.Format("`{0}` = {1}, ", next.Key, SqlType.GetStringInSqlFormat(next.Value)));
            // удаляем последние пробел и запятую
            query = query.Substring(0, query.Length - 2) + " ";
            query += String.Format("WHERE `{0}` = {1};", _nameOfIdField, SqlType.GetStringInSqlFormat(_valueOfIdField));
            SqlQuery.Execute(query);
        }

        /// <summary>
        /// Проверяет существует ли данное поле (столбец) таблицы
        /// </summary>
        /// <param name="nameOfField">Название поля (столбца) таблицы</param>
        /// <returns>Результат проверки (true - существует, false - не существует)</returns>
        private bool FieldExist(string nameOfField)
        {
            string query = String.Format("SELECT `{0}` FROM `{1}` WHERE 0", nameOfField, _nameOfTable);
            return SqlQuery.TryExecute(query);
        }

        /// <summary>
        /// Проверяет поле перед записью в него значения - если оно не существует и при этом выбран режим создания полей при их отсутствии (CreateFieldsIfNotExist == true)
        /// будет создано соотвествующее поле, если поле не существует и (CreateFieldsIfNotExist == false) произойдет ошибка
        /// </summary>
        /// <param name="nameOfField">Название поля (столбца) таблицы</param>
        /// <param name="valueOfField">Значение данного поля</param>
        /// <param name="typeOfField">Тип данного поля</param>
        private void PrepareField(string nameOfField, object valueOfField, Type typeOfField = null)
        {
            if (CreateFieldsIfNotExist)
            {
                // проверка существования поля в таблице, если поля нет, то создаем новое поле
                if (!FieldExist(nameOfField))
                {
                    string sqlType = SqlType.GetTypeInSqlFormat(typeOfField ?? valueOfField.GetType());
                    string query = String.Format("ALTER TABLE `{0}` ADD `{1}` {2} DEFAULT NULL", _nameOfTable, nameOfField, sqlType);
                    SqlQuery.TryExecute(query);
                }
            }
            else
            {
                if (!FieldExist(nameOfField))
                {
                    MessageBox.Show(String.Format("Field '{0}' not exist", nameOfField));
                }
            }
        }

        /// <summary>
        /// Создает запись в таблице на SQL сервере (вводится только значение уникального поля)
        /// </summary>
        private void Create()
        {
            PrepareField(_nameOfIdField, _valueOfIdField);

            string valueOfIdField = SqlType.GetStringInSqlFormat(_valueOfIdField);
            string query = String.Format("INSERT INTO `{0}` (`{1}`) VALUES ({2});", _nameOfTable, _nameOfIdField, valueOfIdField);
            SqlQuery.TryExecute(query);
        }

        /// <summary>
        /// Удаляет текущую запись в таблице на SQL сервере
        /// </summary>
        public void Remove()
        {
            // запись удалена - очищаем список запросов
            _setOfNameValue.Clear();

            string valueOfIdField = SqlType.GetStringInSqlFormat(_valueOfIdField);
            string query = String.Format("DELETE FROM `{0}` WHERE `{1}` = {2};", _nameOfTable, _nameOfIdField, valueOfIdField);

            SqlQuery.TryExecute(query);
        }

        /// <summary>
        /// Сохраняет текущую запись в таблице на SQL сервер
        /// </summary>
        public void Save()
        {
            if (!Exist)
                Create();
            Update();
            // запись сохранена удаляем список запросов
            _setOfNameValue.Clear();
        }

        /// <summary>
        /// Задает значение полям таблицы
        /// </summary>
        /// <param name="nameOfField">Название поля таблицы</param>
        /// <param name="valueOfField">Значение для данного поля таблицы</param>
        /// <param name="typeOfField">Задать тип данных для данного поля (по умолчанию тип данных определяется по значению поля)</param>
        public void SetField(string nameOfField, object valueOfField, Type typeOfField = null)
        {
            PrepareField(nameOfField, valueOfField, typeOfField);

            // если данное поле уже было установлено то удаляем старую пару
            if (_setOfNameValue.Keys.Contains(nameOfField))
                _setOfNameValue.Remove(nameOfField);

            // добавляем новую пару
            _setOfNameValue.Add(nameOfField, valueOfField);
        }

        /// <summary>
        /// Очищает все ранее установленные значения полей
        /// </summary>
        public void ClearAllSets()
        {
            _setOfNameValue.Clear();
        }
    }

    /// <summary>
    /// Класс - объектное представление для получения данных из таблицы
    /// </summary>
    public class SqlSelect
    {
        /// <summary>
        /// Массив строк хранящий запросы пользователя
        /// </summary>
        private readonly List<string> _selectQuery = new List<string>();
        /// <summary>
        /// Название таблицы - источника данных
        /// </summary>
        private readonly string _nameOfTable;

        /// <summary>
        /// Логика объединения запросов (true - AND либо false - OR) по умолчанию true - AND
        /// </summary>
        public bool TypeOfLogicIsAnd { get; set; }

        /// <summary>
        /// Название поля по которому происходит сортировка результатов запроса
        /// </summary>
        public string OrderByField { get; set; }

        /// <summary>
        /// Направление сортировки результатов поиска, по умолчанию true - сортировка по возрастанию,
        /// false - сортировка по убыванию
        /// </summary>
        public bool SortIsAscending { get; set; }

        /// <summary>
        /// Создает новый объект для получения данных из таблицы
        /// </summary>
        /// <param name="nameOfTable">Название таблицы - источника данных</param>
        public SqlSelect(string nameOfTable)
        {
            // установки по умолчанию
            SortIsAscending = true;
            TypeOfLogicIsAnd = true;

            _nameOfTable = nameOfTable;
        }

        /// <summary>
        /// Данные - результат запроса
        /// </summary>
        public DataTable DataTable { get; private set; }

        /// <summary>
        /// Требование - необходимость равенства значения поля в таблице с заданным значением
        /// </summary>
        /// <param name="nameOfField">Название поля в таблице</param>
        /// <param name="comparison">Заданное значение</param>
        public void Equal(string nameOfField, Object comparison)
        {
            string str = SqlType.GetStringInSqlFormat(comparison);
            string query = String.Format("(`{0}` = {1})", nameOfField, str);

            _selectQuery.Add(query);
        }

        /// <summary>
        /// Требование - необходимость того, чтобы заданное значение было подстрокой в значении поле в таблице
        /// </summary>
        /// <param name="nameOfField">Название поля в таблице</param>
        /// <param name="comparison">Заданное значение (подстрока)</param>
        public void Like(string nameOfField, Object comparison)
        {
            string str = SqlType.GetStringInSqlFormat(comparison);

            // удаляем кавычки, добавляем проценты, добавляем кавычки
            if (str != "NULL")
                str = String.Format("'%{0}%'", str.Substring(1, str.Length - 2));

            string query = String.Format("(`{0}` LIKE {1})", nameOfField, str);

            _selectQuery.Add(query);
        }

        /// <summary>
        /// Требование - необходимость того, чтобы значение поля в таблице было меньше либо равно заданному значению
        /// </summary>
        /// <param name="nameOfField">Название поля в таблице</param>
        /// <param name="comparison">Заданное значение</param>
        public void LessOrEqual(string nameOfField, Object comparison)
        {
            string str = SqlType.GetStringInSqlFormat(comparison);
            string query = String.Format("(`{0}` <= {1})", nameOfField, str);

            _selectQuery.Add(query);
        }

        /// <summary>
        /// Требование - необходимость того, чтобы значение поля в таблице было больше либо равно заданному значению
        /// </summary>
        /// <param name="nameOfField">Название поля в таблице</param>
        /// <param name="comparison">Заданное значение</param>
        public void MoreOrEqual(string nameOfField, Object comparison)
        {
            string str = SqlType.GetStringInSqlFormat(comparison);
            string query = String.Format("(`{0}` >= {1})", nameOfField, str);

            _selectQuery.Add(query);
        }

        /// <summary>
        /// Возвращает значение выбранного поля из выбранной строки результатов запроса 
        /// </summary>
        /// <param name="nameOfField">Название поля в таблице</param>
        /// <param name="indexOfRow">Порядковый номер строки из результатов запроса (по умолчанию == 0)</param>
        /// <returns>Значение выбранного поля выбранной строки</returns>
        public object GetValueOfField(string nameOfField, int indexOfRow = 0)
        {
            int currIndex = DataTable.Columns.IndexOf(nameOfField);
            object res = "";
            if (currIndex != -1)
                res = DataTable.Rows[indexOfRow].ItemArray[currIndex];
            return res;
        }

        /// <summary>
        /// Количество найденных записей в таблице
        /// </summary>
        public int CountOfRows
        {
            get { return DataTable.Rows.Count; }
        }

        /// <summary>
        /// Получает данные запроса
        /// </summary>
        public void RetrieveData()
        {
            string query = "";

            if (_selectQuery.Count > 0)
            {
                query = String.Format("SELECT * FROM `{0}` WHERE", _nameOfTable);
                query = _selectQuery.Aggregate(query, (current, next) => current + String.Format(" {0} {1}", next, TypeOfLogicIsAnd ? "AND" : "OR"));

                // удаляем последний AND или OR
                query = TypeOfLogicIsAnd ? query.Substring(0, query.Length - 4) : query.Substring(0, query.Length - 3);
            }
            else
            {
                query = String.Format("SELECT * FROM `{0}`", _nameOfTable);
            }

            if (OrderByField != null)
                query += String.Format(" ORDER BY `{0}` {1};", OrderByField, SortIsAscending ? "ASC" : "DESC");
            else
            {
                query += ";";
            }
            DataTable = SqlQuery.Execute(query);
        }
    }

    public static class ClientLibrary
    {
        public static string GetAuthorOfComputer()
        {
            string author = "";

            List<string> macAddresses = GetMacAddressOfCurrentComputer();
            foreach (string macAddress in macAddresses)
            {
                SqlSelect sqlSelect = new SqlSelect(TbPeopleHardwareAddress.Name);
                sqlSelect.Like(TbPeopleHardwareAddress.MacAddress, macAddress);
                sqlSelect.RetrieveData();

                if (sqlSelect.CountOfRows == 1)
                {
                    author = sqlSelect.GetValueOfField(TbPeopleHardwareAddress.SurnameAndName).ToString();
                    break;
                }
            }

            return author != "" ? author : macAddresses[0];
        }

        private static List<string> GetMacAddressOfCurrentComputer()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            List<string> res = new List<string>();
            foreach (NetworkInterface adapter in nics)
            {
                string macAdress = "";
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    PhysicalAddress address = adapter.GetPhysicalAddress();
                    byte[] bytes = address.GetAddressBytes();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        macAdress += bytes[i].ToString("X2");
                        if (i != bytes.Length - 1)
                            macAdress += "-";
                    }
                    res.Add(macAdress);
                }
            }
            return res;
        }

        public static bool CheckServerStatus()
        {
            return SqlQuery.TryExecute("SHOW DATABASES");
        }

    }
}
