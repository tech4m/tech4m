using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using tech4mUI.Controllers;
using tech4mEntity;
using tech4mEntity.Extensions;

namespace tech4mUI
{
    public class NotificationHub : Hub
    {
        public string Activate()
        {
            return new BaseController().RenderRazorViewToString(
                       "FB_NotificationItem",
                       NotificationMessage.
                       NewMessage("Receiving Notifications"));

        }
    }
}