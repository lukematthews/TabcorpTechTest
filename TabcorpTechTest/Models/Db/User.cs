﻿namespace TabcorpTechTest.Models.Db
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
