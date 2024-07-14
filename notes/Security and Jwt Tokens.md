# Security

If you want to know how to log in with any given user take a look at the `Seed.cs` class to see what the generated password is. It should be `Pa$$w0rd`.

Its always good to store the password hash as opposed to a password. This is a one way encryption method so one cannot decrypt back from the password to the hash. However this is not enough because if a user uses a weak well known password the hash will be recognisable and if the database is compromized and the attacker notices that Jack and Jill have the same hash and that hash maps back to a well known password online the attacker can use that password to gain access to both user's account.. Online there are dictionaries of hashes that map back to commonly used passwords based on common algorithms used to hash them. Salting protects against [dictionary attacks](https://en.wikipedia.org/wiki/Dictionary_attack).

Hashing and salting (another randomized string) a password means that even though Jack and Jill have the same hashed password the result of the salt means that there is no way to determine that their passwords are the same.

So the database has both a password hash field and a password salt field.

See the `AccountController` to see how the user registers and logs in. Hashing is used using `HMACSHA512`. The `hmac.Key` will be used to obtain the `PasswordSalt`.

```csharp
	using var hmac = new HMACSHA512();
	var user = new AppUser
	{
		Username = registerDto.Username.ToLower(),
		PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
		PasswordSalt = hmac.Key
	};
```

Now during login we can check the login password to make sure that it corresponds to the one we have registered based on the hash and the hash salt.

```csharp
	using var hmac = new HMACSHA512(user.PasswordSalt);
	var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
	for (int i = 0; i < computedHash.Length; i++)
	{
		if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
	}
```

For insight into how the token is created have a look at the `TokenService` which allows us to abstract the creation of the token away from the rest of the application. This will also allow us to swap out the service during unit testing.

# JSON Web Tokens

This should be a REST service. Hence one does not want to hold state. Therefore a JSON web token is preferred as a form of authentication. Once the user has a token, the service does not need to hit the database on every attempt.

Creating the token is also abstracted from the rest of the application using a `TokenService`. Use [this site to decode the JWT](https://jwt.ms/). The service is inject as `AddScoped` because it is not necessary on every call. It is only necessary when we need to create a token.

The `TokenService` reads from the configuration from `appSettings` `TokenKey` to retrieve the key used to encrypt the token. The token will consist of a number of claims amongst other properties.

The `Authorize` attribute above the controller method handles whether the bearer token is going to be asserted against or not.

Now the service needs to know how to authenticate. This is done in two parts:

- Add a service
- Add some middleware

The `IdentityServiceExtensions` middleware class which uses the `Microsoft.AspNetCore.Authentication.JwtBearer` package defines how the system will validate the token. Note how it reads the `TokenKey` from the APIs config.

The following needs to be applied in the order that it is presented here. The first line finds out who you are. The second line asserts that you have the rights to access the resource.

```csharp
app.UseAuthentication();
app.UseAuthorization();
```