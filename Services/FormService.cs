using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MTCBackend.Data;
using MTCBackend.DTOs;
using MTCBackend.Interfaces;
using MTCBackend.Models;

namespace MTCBackend.Services
{
    public class FormService : IFormService
    {
        private readonly MTCContext _context;

        public FormService(MTCContext context)
        {
            _context = context;
        }

        public async Task<FormDTO> GetFormById(Guid formId)
        {
            var form = await _context.Forms
                .Include(f => f.Fields)
                .FirstOrDefaultAsync(f => f.Id == formId);

            if (form == null) throw new Exception("Form not found");

            return new FormDTO
            {
                Id = form.Id,
                Name = form.Name,
                PatientId = form.PatientId,
                Fields = form.Fields.Select(field => new FormFieldDTO
                {
                    Id = field.Id,
                    FieldType = field.FieldType,
                    FieldName = field.FieldName
                }).ToList()
            };
        }

        public async Task<List<FormDTO>> GetFormsByPatientId(Guid patientId)
        {
            var forms = await _context.Forms
                .Where(f => f.PatientId == patientId)
                .Include(f => f.Fields)
                .ToListAsync();

            return forms.Select(form => new FormDTO
            {
                Id = form.Id,
                Name = form.Name,
                PatientId = form.PatientId,
                Fields = form.Fields.Select(field => new FormFieldDTO
                {
                    Id = field.Id,
                    FieldType = field.FieldType,
                    FieldName = field.FieldName
                }).ToList()
            }).ToList();
        }

        public async Task<Guid> CreateForm(FormDTO formDto)
        {
            var form = new Form
            {
                Name = formDto.Name,
                PatientId = formDto.PatientId,
                Fields = formDto.Fields.Select(field => new FormField
                {
                    FieldType = field.FieldType,
                    FieldName = field.FieldName
                }).ToList()
            };

            _context.Forms.Add(form);
            await _context.SaveChangesAsync();

            return form.Id;
        }

        public async Task CreateFormSubmission(FormSubmissionDTO submissionDto)
        {
            var submission = new FormSubmission
            {
                FormId = submissionDto.FormId,
                PatientId = submissionDto.PatientId,
                SignatureId = submissionDto.SignatureId,
                UploadedFiles = submissionDto.UploadedFiles.Select(file => new UploadedFile
                {
                    FilePath = file.FilePath
                }).ToList()
            };

            _context.FormSubmissions.Add(submission);
            await _context.SaveChangesAsync();
        }
    }
}
