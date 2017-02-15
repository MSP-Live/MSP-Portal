using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Configuration;
using MSP_Portal.MessageProcessors;

namespace MSP_Portal.Controllers
{
    /// <summary>
    /// Контроллео обработки сообщений.
    /// </summary>
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        /// <summary>
        /// Хранилище конфигурации.
        /// </summary>
        protected readonly IConfigurationRoot configuration;

        /// <summary>
        /// Словарь с обработчиками типов команд.
        /// </summary>
        protected Dictionary<string, IMessageProcessor> _processors;
        
        /// <summary>
        /// Публичный конструктор контроллера.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        public MessagesController(IConfigurationRoot configuration)
        {
            // Передача параметров конфигурации.
            this.configuration = configuration;
            // Конфигурирование обработчиков по умолчанию.
            ProcessorsInit();
        }

        /// <summary>
        /// Метод конфигурирования обработчиков по умолчанию.
        /// </summary>
        protected virtual void ProcessorsInit()
        {
            // Инициализация словаря с набором процессоров-обработчиков.
            Dictionary<string, IMessageProcessor> processors = new Dictionary<string, IMessageProcessor>();
            // Добавление обработчика операций над пользователем.
            processors.Add("user", new UsersProcessor());
            // Установка конфигурации обработчиков.
            ProcessorsInit(processors);
        }

        /// <summary>
        /// Метод установки конфигурации обработчиков.
        /// </summary>
        /// <param name="processors">Конфигурация обработчиков.</param>
        public virtual void ProcessorsInit(Dictionary<string, IMessageProcessor> processors)
        {
            // Передача параметров конфигурации.
            _processors = processors;
        }
        
        /// <summary>
        /// Метод обработки POST-запросов.
        /// </summary>
        /// <param name="activity">Сообщение формата Bot Framework</param>
        /// <returns>HTTP код ответа.</returns>
        [Authorize(Roles = "Bot")]
        [HttpPost]
        public virtual async Task<StatusCodeResult> Post([FromBody]Activity activity)
        {
            // Инициализация буффера под HTTP код для ответа.
            HttpStatusCode responseCode = HttpStatusCode.OK;
            // Определение типа сообщения.
            switch (activity.Type)
            {
                // Сообщение от пользователя.
                case ActivityTypes.Message:
                    // Инициализация буффера под текст сообщения ответа.
                    Activity answer = activity.CreateReply();
                    // Получение учётных данных из конфигурации.
                    MicrosoftAppCredentials appCredentials = new MicrosoftAppCredentials(configuration);
                    // Подключение к Bot Framework'у.
                    using (ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl), appCredentials))
                    {
                        try
                        {
                            // Получение идентификатора диалога.
                            string id = activity.Conversation.Id;
                            // Разбиение сообщения на части.
                            string[] part = activity.Text.Split(' ');
                            // Разбиение заголовка на части.
                            string[] header = part[0].Split('.');
                            // Получение типа модуля команд.
                            string type = header[0].ToLower();
                            // Получение типа команды.
                            string operation = header[1].ToLower();
                            // Получение аргументов сообщения.
                            string[] args = part[1].Split(';');
                            // Проверка на наличие модуля обработчиков.
                            if (_processors.ContainsKey(type))
                            {
                                // Выполнение обработки сообщения.
                                _processors[type].Process(new Message(id, type, operation, args), ref answer);
                            }
                            else
                            {
                                // Выбрасывание исключения об отсутствии поддержки модуля обработчика.
                                throw new NotImplementedException("Not supported type.");
                            }
                        }
                        // Отлавливание исключений.
                        catch (Exception e)
                        {
                            // Установка кода ошибки.
                            responseCode = HttpStatusCode.InternalServerError;
                            // Возврат информации об ошибке.
                            answer.Text = "ERROR: " + e.Message;
                        }
                        // Отправка ответного сообщения.
                        await connector.Conversations.ReplyToActivityAsync(answer);
                    }
                    break;
            }
            // Возврат кода результата выполнения.
            return new StatusCodeResult((int)responseCode);
        }
    }
}
