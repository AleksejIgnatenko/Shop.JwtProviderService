using Grpc.Core;
using Shop.JwtProviderService.Abstractions;

namespace Shop.JwtProviderService.Services
{
    public class JwtService : JwtToken.JwtTokenBase
    {
        private readonly IJwtProvider _jwtProvider;

        public JwtService(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        public override Task<JwtReply> GenerateJwtToken(JwtRequest request, ServerCallContext context)
        {
            Console.WriteLine(request.Id);
            var token = _jwtProvider.GenerateToken(request);
            return Task.FromResult(new JwtReply
            {
                Token = token,
            });
        }
    }
}
