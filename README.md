## Web Sockets Tools

### Authenticator Route Web Socket

#### Server

```csharp
private const string SecretKey = "MySuperSecretKey12345";


protected override void OnOpen()
    {
        // Extrair token JWT da query string
        var token = Context.QueryString["token"];

        if (IsTokenValid(token, out var username))
        {
            _username = username;
            Console.WriteLine($"Conexão autenticada de {_username}");
            Send($"Bem-vindo à rota autenticada, {_username}!");
        }
        else
        {
            Console.WriteLine("Tentativa de acesso não autorizado.");
            Context.WebSocket.Close(CloseStatusCode.PolicyViolation, "Token inválido ou não fornecido.");
        }
    }



private bool IsTokenValid(string token, out string username)
    {
        username = null;

        if (string.IsNullOrEmpty(token))
            return false;

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, // Não validamos o emissor neste exemplo
                ValidateAudience = false, // Não validamos o público neste exemplo
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            username = principal.Identity.Name;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao validar o token: {ex.Message}");
            return false;
        }
    }
```
#### Cliente

```csharp
//Gerar token para o cliente
 private const string SecretKey = "MySuperSecretKey12345"; // Deve ser uma chave segura (256 bits)

    public static string GenerateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
            Expires = DateTime.UtcNow.AddHours(1), // Token válido por 1 hora
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


static void Main(string[] args)
    {
        var server = new WebSocketServer("ws://localhost:8080");

        // Rota protegida por autenticação JWT
        server.AddWebSocketService<AuthenticatedWebSocketBehavior>("/secure");

        server.Start();
        Console.WriteLine("Servidor iniciado em ws://localhost:8080");
        Console.WriteLine("Pressione qualquer tecla para encerrar...");
        Console.ReadKey();

        server.Stop();
    }
```
