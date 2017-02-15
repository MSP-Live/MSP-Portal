using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Bot.Connector;

namespace MSP_Portal.MessageProcessors
{
    /// <summary>
    /// Класс модуля обработчика действий с профилем пользователя.
    /// </summary>
    public class UsersProcessor : MessageProcessor
    {
        /// <summary>
        /// Публичный конструктор с конфигурацией обработчиков по умолчанию.
        /// </summary>
        public UsersProcessor()
        {
            // Вызов конфигуратора по умолчанию.
            ProcessorsInit();
        }

        /// <summary>
        /// Метод конфигурации обработчиков по умолчанию.
        /// </summary>
        protected override void ProcessorsInit()
        {
            // Инициализация массива обработчиков.
            Dictionary<string, MessageProcess> processors = new Dictionary<string, MessageProcess>();
            // Добавление функции смены пароля пользователя.
            processors.Add("changepassword",
                new MessageProcess(ChangePassword));
            // Добавление функции смены номера телефона.
            processors.Add("changephonenumber",
                new MessageProcess(ChangePhoneNumber));
            // Добавление функции смены города.
            processors.Add("changecity",
                new MessageProcess(ChangeCity));
            // Передача значения конфигурации.
            ProcessorsInit(processors);
        }

        /// <summary>
        /// Функция смены пароля пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="args">Аргументы команды.</param>
        /// <param name="activity">Ответное сообщение.</param>
        public virtual void ChangePassword(string id, string[] args, ref Activity activity)
        {
            // Открытие подключения к базе данных.
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                // Получение профиля пользователя.
                Entities.User user = db.UsersCollection.FirstOrDefault(x => x.ChatIds.Contains(id));
                // Проверка наличия профиля пользователя.
                if (user != null)
                {
                    // Проверка сложности пароля.
                    if (PasswordChecker(args[0]))
                    {
                        // Вычисление хеша пароля.
                        user.PasswordHash = BitConverter.ToString(System.Security.Cryptography.SHA256.Create()
                            .ComputeHash(Encoding.UTF8.GetBytes(args[0])));
                        // Созранение изменений.
                        db.SaveChanges();
                        // Передача текста ответного сообщения.
                        activity.Text = "Password successful changed.";
                    }
                    else
                    {
                        // Выбрасывание исключения о несоответствии пароля требованиям.
                        throw new ArgumentException("Not correct password. Password should contains minimum one digit, capital, small letters and consist from more than 8 symbol.");
                    }
                }
                else
                {
                    // Выбрасывание исключения об отсутствии профиля пользователя.
                    throw new KeyNotFoundException("User not found.");
                }
            }
        }

        /// <summary>
        /// Метод проверки соответсвия пароля.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <returns>Индикатор соответствия.</returns>
        protected virtual bool PasswordChecker(string password)
        {
            // Инициализация регулярного выражения.
            System.Text.RegularExpressions.Regex checker = new System.Text.RegularExpressions.Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}");
            // Проверка соответствия.
            return checker.IsMatch(password);
        }

        /// <summary>
        /// Метод смены номера телефона.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="args">Аргументы команды.</param>
        /// <param name="activity">Ответное сообщение.</param>
        public virtual void ChangePhoneNumber(string id, string[] args, ref Activity activity)
        {
            // Открытие подключения к базе данных.
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                // Получение профиля пользователя.
                Entities.User user = db.UsersCollection.First(x => x.ChatIds.Contains(id));
                // Проверка наличия профиля пользователя.
                if (user != null)
                {
                    // Проверка соответствия номера телефона.
                    if (PhoneNumberChecker(args[0]))
                    {
                        // Передача значения.
                        user.PhoneNumber = args[0];
                        // Сохранение изменений.
                        db.SaveChanges();
                        // Передача текста ответного сообщения.
                        activity.Text = "Phone number successful changed.";
                    }
                    else
                    {
                        // Выбрасывание исключения о несоответствии пароля требованиям.
                        throw new ArgumentException("Not correct phone number.");
                    }
                }
                else
                {
                    // Выбрасывание исключения об отсутствии профиля пользователя.
                    throw new KeyNotFoundException("User not found.");
                }
            }
        }

        /// <summary>
        /// Метод проверки правильности ввода номера телефона.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <returns>Индикатор правильности ввода.</returns>
        protected virtual bool PhoneNumberChecker(string phone)
        {
            // Инициализация регулярного выражения.
            System.Text.RegularExpressions.Regex checker = new System.Text.RegularExpressions.Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");
            // Проверка соответствия.
            return checker.IsMatch(phone);
        }

        /// <summary>
        /// Метод смены города.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="args">Аргументы команды.</param>
        /// <param name="activity">Ответное сообщение.</param>
        public virtual void ChangeCity(string id, string[] args, ref Activity activity)
        {
            // Открытие подключения к базе данных.
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                // Получение профиля пользователя.
                Entities.User user = db.UsersCollection.First(x => x.ChatIds.Contains(id));
                // Проверка наличия профиля пользователя.
                if (user != null)
                {
                    // Инициализация текстового буффера.
                    StringBuilder builder = new StringBuilder();
                    // Складывание аргументов команды.
                    foreach (string buffer in args)
                    {
                        builder.Append(buffer).Append(' ');
                    }
                    builder.Remove(builder.Length - 1, 1);
                    // Передача идентифкатора города.
                    user.CityId = GetCityId(builder.ToString());
                    // Сохранение изменений.
                    db.SaveChanges();
                    // Передача текста ответного сообщения.
                    activity.Text = "City successful changed.";
                }
                else
                {
                    // Выбрасывание исключения об отсутствии профиля пользователя.
                    throw new KeyNotFoundException("User not found.");
                }
            }
        }

        /// <summary>
        /// Метод получение идентификатора города по наименованию.
        /// </summary>
        /// <param name="cityName">Название города.</param>
        /// <returns>Идентификатор города.</returns>
        protected virtual int GetCityId(string cityName)
        {
            // Открытие подключения к базе данных.
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                // Получение информации о городе.
                Entities.City city = db.CitiesCollection.FirstOrDefault(x => x.Name == cityName);
                // Проверка наличия информации.
                if (city == null)
                {
                    // TODO: уведомление о необходимости добавления города.
                }
                // Возврат идентификатора.
                return city.Id;
            }
        }
    }
}
