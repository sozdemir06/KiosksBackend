using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        public string LogDetail { get; set; }
        public DateTime Date { get; set; }
        public string Audit { get; set; }
    }
}