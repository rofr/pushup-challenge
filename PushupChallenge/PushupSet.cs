using System;
using SQLite;

namespace PushupChallenge
{
    public class PushupSet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Repetitions { get; set; }
        public DateTime When { get; set; }
    }
}
