//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lisa.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class report_info
    {
        public string name { get; set; }
        public Nullable<System.DateTime> date_of_creating { get; set; }
        public string description { get; set; }
        public string path { get; set; }
        public int id { get; set; }

        public report_info(string name, DateTime? date_of_creating, string description, string path)
        {
            this.id = ReportBaseEntities1.GetContext().report_info.ToList().Count;
            this.name = name;
            this.date_of_creating = date_of_creating;
            this.description = description;
            this.path = path;
        }
        public report_info()
        {

        }
    }
}

