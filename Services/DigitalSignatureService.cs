using MTCBackend.Data;
using MTCBackend.Interfaces;
using MTCBackend.Models;
using System.Security.Cryptography;
using System.Text;

namespace MTCBackend.Services
{
    public class DigitalSignatureService : IDigitalSignatureService
    {
        private readonly MTCContext _context;

        public DigitalSignatureService(MTCContext context)
        {
            _context = context;
        }

        public async Task<Guid> CaptureSignatureAsync(Guid formId, Guid patientId, string base64SignatureData)
        {
            // Generate hash for the signature for integrity verification
            var hash = GenerateHash(base64SignatureData);

            var digitalSignature = new DigitalSignature
            {
                Id = Guid.NewGuid(),
                FormId = formId,
                PatientId = patientId,
                Base64SignatureData = base64SignatureData,
                SignedDate = DateTime.UtcNow,
                Hash = hash
            };

            _context.DigitalSignatures.Add(digitalSignature);
            await _context.SaveChangesAsync();

            return digitalSignature.Id;
        }

        public async Task<bool> VerifySignatureAsync(Guid signatureId, string signatureHash)
        {
            var signature = await _context.DigitalSignatures.FindAsync(signatureId);

            if (signature == null)
                return false;

            // Verify the hash of the stored signature matches the provided hash
            return signature.Hash == signatureHash;
        }

        private string GenerateHash(string base64SignatureData)
        {
            using (var sha256 = SHA256.Create())
            {
                var byteArray = Encoding.UTF8.GetBytes(base64SignatureData);
                var hashBytes = sha256.ComputeHash(byteArray);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
