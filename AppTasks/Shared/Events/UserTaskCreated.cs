﻿namespace Shared.Events
{
    public class UserTaskCreated
    {
        public string? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UserMail { get; set; } = string.Empty;
    }
}
