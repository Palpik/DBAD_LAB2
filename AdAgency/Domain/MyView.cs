using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class MyView
    {
        public int НомерЗаказа { get; set; }
        public string Расположение { get; set; } = null!;
        public decimal Стоимость { get; set; }
        public string ТипРекламы { get; set; } = null!;
        public string ОписаниеМеста { get; set; } = null!;
        public string Описание { get; set; } = null!;
    }
}
