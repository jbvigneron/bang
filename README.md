# pang
Inspired by Bang game

## API development secrets

The API uses `Jwt:Key` to sign authentication tokens. Keep this key outside the repository.

For local development, create a random key and store it with .NET User Secrets:

```powershell
$bytes = New-Object byte[] 64
$rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
try {
    $rng.GetBytes($bytes)
    $jwtKey = [Convert]::ToBase64String($bytes)
    dotnet user-secrets set "Jwt:Key" $jwtKey --project api\Bang.WebApi\Bang.WebApi.csproj
} finally {
    $rng.Dispose()
    [Array]::Clear($bytes, 0, $bytes.Length)
    Remove-Variable jwtKey -ErrorAction SilentlyContinue
}
```

For deployments, provide `Jwt__Key` through the platform's environment variables or secret manager. Do not add signing keys to `appsettings*.json` or commit them to source control.
