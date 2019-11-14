using Ganss.XSS;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Hubs
{
    public class ChatHub : Hub
    {
        public static List<ChatClient> clients = new List<ChatClient>();

        public IClientProxy GetGroup()
        {
            var client = GetClient();

            return Clients.Group(client.ChatRoom);
        }

        public ChatClient GetClient()
        {
            return clients.Find(x => x.Identifier == Context.UserIdentifier);
        }

        public IEnumerable<ChatClient> GetClientsInRoom(string chatRoom)
        {
            return clients.Where(x => x.ChatRoom == chatRoom);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserLeavedEvent();

            return base.OnDisconnectedAsync(exception);
        }

        public void Register(string chatRoomName)
        {
            if (!string.IsNullOrWhiteSpace(chatRoomName))
            {
                UserJoinedEvent(chatRoomName);
            }
        }

        public void GetClientList()
        {
            var client = GetClient();

            if(client != null)
            {
                Clients.Client(client.ConnectionId).SendAsync("ReceiveClientList", GetClientsInRoom(client.ChatRoom));
            }
        }

        public void SendMessage(string message)
        {
            var client = GetClient();

            if (client != null && !string.IsNullOrWhiteSpace(message))
            {
                var sanitizer = new HtmlSanitizer();
                sanitizer.AllowedTags.Clear(); // No Tags Allowed
                var sanitizedMessage = sanitizer.Sanitize(message);

                if (!string.IsNullOrWhiteSpace(sanitizedMessage))
                {
                    Clients.Group(client.ChatRoom).SendAsync("ReceiveMessage", client.Name, sanitizedMessage);
                }
            }
        }

        public void UserJoinedEvent(string chatRoom)
        {
            var newClient = new ChatClient()
            {
                ConnectionId = Context.ConnectionId,
                Name = Context.User.Identity.Name,
                Identifier = Context.UserIdentifier,
                ChatRoom = chatRoom
            };

            clients.Add(newClient);
            Groups.AddToGroupAsync(Context.ConnectionId, chatRoom);
            Clients.Group(chatRoom).SendAsync("UserJoined", newClient.Name, newClient.Identifier);
        }

        public void UserLeavedEvent()
        {
            var client = GetClient();

            clients.Remove(client);
            Groups.RemoveFromGroupAsync(client.Identifier, client.ChatRoom);
            Clients.Group(client.ChatRoom).SendAsync("UserLeaved", client.Name, client.Identifier);
        }
    }

    public class ChatClient
    {
        public string ConnectionId { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string ChatRoom { get; set; }
    }
}