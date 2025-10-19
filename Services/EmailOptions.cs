﻿namespace RazorPagesBook.Services
{
    public class EmailOptions
    {
        public string Host { get; set; } = "";
        public int Port { get; set; } = 587;
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public string From { get; set; } = "";
        public string FromName { get; set; } = "RpMovie";
        public bool UseSsl { get; set; } = true;
    }

}
