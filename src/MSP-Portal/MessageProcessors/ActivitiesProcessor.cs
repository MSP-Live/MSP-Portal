using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector;

namespace MSP_Portal.MessageProcessors
{
    /// <summary>
    /// Класс модуля обработчика действий с активностями.
    /// </summary>
    public class ActivitiesProcessor : MessageProcessor
    {
        /// <summary>
        /// Публичный конструктор с конфигурацией обработчиков по умолчанию.
        /// </summary>
        public ActivitiesProcessor()
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
            // Добавление функции добавления активности.
            processors.Add("add",
                new MessageProcess(Add));
            // Добавление функции обновления активности.
            processors.Add("update",
                new MessageProcess(Update));
            // Передача значения конфигурации.
            ProcessorsInit(processors);
        }

        /// <summary>
        /// Функция добавления активности.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="args">Аргументы команды.</param>
        /// <param name="activity">Ответное сообщение.</param>
        public virtual void Add(string id, string[] args, ref Activity activity)
        {
            // Открытие подключения к базе данных.
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                // Получение профиля пользователя.
                Entities.User user = db.UsersCollection.FirstOrDefault(x => x.ChatIds.Contains(id));
                // Проверка наличия профиля пользователя.
                if (user != null)
                {
                    // Инициализируем новую активность.
                    Entities.Activity newActivity = new Entities.Activity();
                    // Передача значения идентификатора пользователя.
                    newActivity.UserId = user.Id;
                    // Проверка значения параметров команды.
                    if (args.Length >= 2)
                    {
                        // Получение названия активности.
                        newActivity.Name = args[0];
                        // Получение типа активности.
                        newActivity.ActivityTypeId = GetActivityTypeId(args[1]);
                        // Получение заметок.
                        newActivity.Note = args[2];
                        // Добавление активности в базу данных.
                        db.ActivitiesCollection.Add(newActivity);
                        // Сохранение изменений.
                        db.SaveChanges();
                        // Передача текста ответного сообщения.
                        activity.Text = "New activity successful added.";

                    }
                    else
                    {
                        // Выбрасывание исключения о несоответствии входящих параметров.
                        throw new ArgumentException("Not corrected input data.");
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
        /// Метод получения типаактивности.
        /// </summary>
        /// <param name="name">Наименование типа активности.</param>
        /// <returns>Идентификационный номер типа активности.</returns>
        protected virtual int GetActivityTypeId(string name)
        {
            // Открытие подключения к базе данных.
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                // Получение информации о типе активности.
                Entities.ActivityType activityType = db.ActivityTypesCollection.FirstOrDefault(x => x.Name == name);
                // Проверка наличия типа активности.
                if (activityType != null)
                {
                    // Возврат идентификационного номера типа активности.
                    return activityType.Id;
                }
                else
                {
                    // Выбрасывание исключения о несоответствии входных параметров.
                    throw new ArgumentException("Activity type not found.");
                }
            }
        }

        /// <summary>
        /// Метод обновления информации об активности.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="args">Аргументы команды.</param>
        /// <param name="activity">Ответное сообщение.</param>
        public virtual void Update(string id, string[] args, ref Activity activity)
        {
            // Открытие подключения к базе данных.
            using (DBContext.PortalContext db = new DBContext.PortalContext())
            {
                // Получение старой информации об активности.
                Entities.Activity oldActivity = db.ActivitiesCollection.FirstOrDefault(x => x.Id == int.Parse(args[0]));
                // Проверка наличия информации.
                if (oldActivity == null)
                {
                    // Выбраывание исключения об отсутствии активности. 
                    throw new ArgumentException("Activity not found.");
                }
                else
                {
                    // Проверка значения параметров команды.
                    if (args.Length >= 2)
                    {
                        // Получение названия активности.
                        oldActivity.Name = args[0];
                        // Получение типа активности.
                        oldActivity.ActivityTypeId = GetActivityTypeId(args[1]);
                        // Получение заметок.
                        oldActivity.Note = args[2];
                        // Добавление активности в базу данных.
                        db.ActivitiesCollection.Add(oldActivity);
                        // Сохранение изменений.
                        db.SaveChanges();
                        // Передача текста ответного сообщения.
                        activity.Text = "Activity successful updated.";

                    }
                    else
                    {
                        // Выбрасывание исключения о несоответствии входящих параметров.
                        throw new ArgumentException("Not corrected input data.");
                    }
                }
            }
        }
    }
}
