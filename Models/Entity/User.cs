//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediaCenter.Models.Entity
{
	using System;
	using System.Collections.Generic;

	public partial class User
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public User()
		{
			this.Managers = new HashSet<Manager>();
		}

		public int ID { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public byte Access { get; set; }
		public string Role => Access == 0 ? "Менеджер" : "Директор";

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Manager> Managers { get; set; }
	}
}