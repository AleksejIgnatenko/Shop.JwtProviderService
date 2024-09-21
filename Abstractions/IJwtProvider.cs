namespace Shop.JwtProviderService.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(JwtRequest jwtRequest);
        Guid GetUserIdFromToken(string jwtToken);
    }
}