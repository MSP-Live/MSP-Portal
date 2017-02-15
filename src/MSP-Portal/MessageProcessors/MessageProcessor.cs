using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace MSP_Portal.MessageProcessors
{
    /// <summary>
    /// Интерфейс обработчика команд.
    /// </summary>
    public interface IMessageProcessor
    {
        /// <summary>
        /// Выполнение обработки сообщения.
        /// </summary>
        /// <param name="message">Входящее сообщение.</param>
        /// <param name="activity">Ответное сообщение.</param>
        void Process(Message message, ref Activity activity);
    }

    /// <summary>
    /// Делегат метода обработки команды.
    /// </summary>
    /// <param name="id">Идентификатор диалога.</param>
    /// <param name="args">Набор аргументов команды.</param>
    /// <param name="activity">Ответное сообщение.</param>
    public delegate void MessageProcess(string id, string[] args, ref Activity activity);

    /// <summary>
    /// Абстрактный класс обработчика сообщений.
    /// </summary>
    public abstract class MessageProcessor : IMessageProcessor
    {
        /// <summary>
        /// Массив с обработчиками команд.
        /// </summary>
        protected Dictionary<string, MessageProcess> _processors { get; set; }

        /// <summary>
        /// Метод конфигурирования обработчиков по умолчанию.
        /// </summary>
        protected abstract void ProcessorsInit();

        /// <summary>
        /// Метод установки конфигурации обработчиков.
        /// </summary>
        /// <param name="processors">Конфигурация обработчиков.</param>
        public virtual void ProcessorsInit(Dictionary<string, MessageProcess> processors)
        {
            // Передача параметров конфигурации.
            _processors = processors;
        }

        /// <summary>
        /// Метод обработки сообщения.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="activity">Ответное сообщение.</param>
        public void Process(Message message, ref Activity activity)
        {
            // Проверка на наличие обработчика команды.
            if (_processors.ContainsKey(message.Operation))
            {
                // Вызов обработчика.
                _processors[message.Operation].Invoke(message.Id, message.Args, ref activity);
            }
            else
            {
                // Выбрасывание исключения об отсутствии поддержки команды.
                throw new NotSupportedException("Not supported operation.");
            }
        }
    }

    /// <summary>
    /// Класс представления сообщения.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Идентификатор диалога.
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Тип команды.
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Название команды.
        /// </summary>
        public string Operation { get; private set; }
        /// <summary>
        /// Аргументы команды.
        /// </summary>
        public string[] Args { get; private set; }
        /// <summary>
        /// Конструктор сообщения.
        /// </summary>
        /// <param name="id">Идентификатор диалога.</param>
        /// <param name="type">Тип команды.</param>
        /// <param name="operation">Название команды.</param>
        /// <param name="args">Аргументы команды.</param>
        public Message(string id, string type, string operation, string[] args)
        {
            // Передача значений.
            Id = id;
            Type = type;
            Operation = operation;
            Args = args;
        }
    }
}
