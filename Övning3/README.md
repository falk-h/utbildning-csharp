Hur man kan hamna i de olika catch-blocken i `Main` (i originalprogrammet): 
- **Filen hittades inte**: verkar aldrig kunna nås eftersom `FileException` fångas av `catch (Exception ex)` i `ProcessFile` och wrappas i en `InvalidOperationException`.

- **Formatfel**: `echo a > numbers.txt && dotnet run`
- **Kan inte dividera med noll**: verkar inte kunna nås eftersom division med noll är definierad för flyttal.
- **Okänt fel**: `rm numbers.txt && dotnet run`