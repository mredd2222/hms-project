namespace MTCBackend.Interfaces
{
    public interface IDigitalSignatureService
    {
        Task<Guid> CaptureSignatureAsync(Guid formId, Guid patientId, string base64SignatureData);
        Task<bool> VerifySignatureAsync(Guid signatureId, string signatureHash);
    }
}
