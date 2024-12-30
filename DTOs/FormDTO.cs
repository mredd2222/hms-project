using System;
using System.Collections.Generic;

namespace MTCBackend.DTOs
{
    public class FormDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid PatientId { get; set; }
        public List<FormFieldDTO>? Fields { get; set; }
    }

    public class FormFieldDTO
    {
        public Guid Id { get; set; }
        public required string FieldType { get; set; }
        public required string FieldName { get; set; }
    }
}
