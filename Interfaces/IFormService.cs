using MTCBackend.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTCBackend.Interfaces
{
    public interface IFormService
    {
        Task<FormDTO> GetFormById(Guid formId);
        Task<List<FormDTO>> GetFormsByPatientId(Guid patientId);
        Task<Guid> CreateForm(FormDTO formDto);
        Task CreateFormSubmission(FormSubmissionDTO submissionDto);
    }
}
