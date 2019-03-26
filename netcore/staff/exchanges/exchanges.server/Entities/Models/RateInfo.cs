using System;

namespace exchanges.server.Entities.Models
{
    ///<summary>Информация о запрошенном курсе</summary>
    public struct RateInfo
    {
        ///<summary>Покупаемая валюта</summary>
        public string To { get; set; }

        ///<summary>Значение курса</summary>
        public decimal Rate { get; set; }

        /// <summary>Время истечения курса</summary>
        public DateTime ExpireAt { get; set; }
    }
}