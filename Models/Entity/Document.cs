//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.IO;
using System.Runtime.CompilerServices;

namespace MediaCenter.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public byte[] File { get; set; }
        public string Load => (File == null) ? "Не загружен" : "Загружен";
    }
}
