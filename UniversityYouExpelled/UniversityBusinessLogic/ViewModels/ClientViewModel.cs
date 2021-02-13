using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using UniversityBusinessLogic.Enums;

namespace UniversityBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        [DataMember]
        [DisplayName("Год Обучения")]
        public int Year { get; set; }
        [DataMember]
        [DisplayName("Логин")]
        public string Login { get; set; }
        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [DataMember]
        [DisplayName("Пользователь")]
        public string UserType { get; set; }
        [DataMember]
        [DisplayName("Статус Блокировки")]
        public int BlockStatus { get; set; }
        [DataMember]
        [DisplayName("Номер телефона")]
        public string Phone { get; set; }
        [DataMember]
        [DisplayName("Почта")]
        public string Email { get; set; }
        [DataMember]
        [DisplayName("DateRegister")]
        public DateTime DateRegister { get; set; }
    }
}