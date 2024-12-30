using System;

namespace MTCBackend.Models
{
    public class FormField
    {
        public Guid Id { get; set; } // Primary key
        public string FieldType { get; set; } // Field type (text, checkbox, etc.)
        public string FieldName { get; set; } // Field name/label
        public Guid FormId { get; set; } // Foreign key to Form
        public Form Form { get; set; } // Navigation property to Form
    }
}
