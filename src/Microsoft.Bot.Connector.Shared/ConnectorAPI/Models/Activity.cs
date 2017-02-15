// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Bot.Connector
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    /// <summary>
    /// An Activity is the basic communication type for the Bot Framework 3.0
    /// protocol
    /// </summary>
    public partial class Activity
    {
        /// <summary>
        /// Initializes a new instance of the Activity class.
        /// </summary>
        public Activity() { }

        /// <summary>
        /// Initializes a new instance of the Activity class.
        /// </summary>
        public Activity(string type = default(string), string id = default(string), DateTime? timestamp = default(DateTime?), DateTime? localTimestamp = default(DateTime?), string serviceUrl = default(string), string channelId = default(string), ChannelAccount from = default(ChannelAccount), ConversationAccount conversation = default(ConversationAccount), ChannelAccount recipient = default(ChannelAccount), string textFormat = default(string), string attachmentLayout = default(string), IList<ChannelAccount> membersAdded = default(IList<ChannelAccount>), IList<ChannelAccount> membersRemoved = default(IList<ChannelAccount>), string topicName = default(string), bool? historyDisclosed = default(bool?), string locale = default(string), string text = default(string), string summary = default(string), IList<Attachment> attachments = default(IList<Attachment>), IList<Entity> entities = default(IList<Entity>), object channelData = default(object), string action = default(string), string replyToId = default(string), object value = default(object), string name = default(string), ConversationReference relatesTo = default(ConversationReference))
        {
            Type = type;
            Id = id;
            Timestamp = timestamp;
            LocalTimestamp = localTimestamp;
            ServiceUrl = serviceUrl;
            ChannelId = channelId;
            From = from;
            Conversation = conversation;
            Recipient = recipient;
            TextFormat = textFormat;
            AttachmentLayout = attachmentLayout;
            MembersAdded = membersAdded;
            MembersRemoved = membersRemoved;
            TopicName = topicName;
            HistoryDisclosed = historyDisclosed;
            Locale = locale;
            Text = text;
            Summary = summary;
            Attachments = attachments;
            Entities = entities;
            ChannelData = channelData;
            Action = action;
            ReplyToId = replyToId;
            Value = value;
            Name = name;
            RelatesTo = relatesTo;
        }

        /// <summary>
        /// The type of the activity
        /// [message|contactRelationUpdate|converationUpdate|typing|endOfConversation|event|invoke]
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// ID of this activity
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// UTC Time when message was sent (set by service)
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Local time when message was sent (set by client, Ex:
        /// 2016-09-23T13:07:49.4714686-07:00)
        /// </summary>
        [JsonProperty(PropertyName = "localTimestamp")]
        public DateTimeOffset? LocalTimestamp { get; set; }

        /// <summary>
        /// Service endpoint where operations concerning the activity may be
        /// performed
        /// </summary>
        [JsonProperty(PropertyName = "serviceUrl")]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// ID of the channel where the activity was sent
        /// </summary>
        [JsonProperty(PropertyName = "channelId")]
        public string ChannelId { get; set; }

        /// <summary>
        /// Sender address
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public ChannelAccount From { get; set; }

        /// <summary>
        /// Conversation
        /// </summary>
        [JsonProperty(PropertyName = "conversation")]
        public ConversationAccount Conversation { get; set; }

        /// <summary>
        /// (Outbound to bot only) Bot's address that received the message
        /// </summary>
        [JsonProperty(PropertyName = "recipient")]
        public ChannelAccount Recipient { get; set; }

        /// <summary>
        /// Format of text fields [plain|markdown] Default:markdown
        /// </summary>
        [JsonProperty(PropertyName = "textFormat")]
        public string TextFormat { get; set; }

        /// <summary>
        /// Hint for how to deal with multiple attachments: [list|carousel]
        /// Default:list
        /// </summary>
        [JsonProperty(PropertyName = "attachmentLayout")]
        public string AttachmentLayout { get; set; }

        /// <summary>
        /// Array of address added
        /// </summary>
        [JsonProperty(PropertyName = "membersAdded")]
        public IList<ChannelAccount> MembersAdded { get; set; }

        /// <summary>
        /// Array of addresses removed
        /// </summary>
        [JsonProperty(PropertyName = "membersRemoved")]
        public IList<ChannelAccount> MembersRemoved { get; set; }

        /// <summary>
        /// Conversations new topic name
        /// </summary>
        [JsonProperty(PropertyName = "topicName")]
        public string TopicName { get; set; }

        /// <summary>
        /// True if the previous history of the channel is disclosed
        /// </summary>
        [JsonProperty(PropertyName = "historyDisclosed")]
        public bool? HistoryDisclosed { get; set; }

        /// <summary>
        /// The language code of the Text field
        /// </summary>
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Content for the message
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Text to display if the channel cannot render cards
        /// </summary>
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonProperty(PropertyName = "attachments")]
        public IList<Attachment> Attachments { get; set; }

        /// <summary>
        /// Collection of Entity objects, each of which contains metadata
        /// about this activity. Each Entity object is typed.
        /// </summary>
        [JsonProperty(PropertyName = "entities")]
        public IList<Entity> Entities { get; set; }

        /// <summary>
        /// Channel-specific payload
        /// </summary>
        [JsonProperty(PropertyName = "channelData")]
        public object ChannelData { get; set; }

        /// <summary>
        /// ContactAdded/Removed action
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        /// <summary>
        /// The original ID this message is a response to
        /// </summary>
        [JsonProperty(PropertyName = "replyToId")]
        public string ReplyToId { get; set; }

        /// <summary>
        /// Open-ended value
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }

        /// <summary>
        /// Name of the operation to invoke or the name of the event
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Reference to another conversation or activity
        /// </summary>
        [JsonProperty(PropertyName = "relatesTo")]
        public ConversationReference RelatesTo { get; set; }

    }
}
