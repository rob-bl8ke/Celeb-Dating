# Use HTTPS (for Angular)

### Self-signed certificate

Whilst we're developing an application, we use self-signed certificates that are not suitable for use on the internet. We'd want proper certificates that other browsers can trust, but because this is our developer machine, we can use self-signed certificates for development.

But we still need to get past our own browser security, which by its very nature will not want to trust

Browser security, by its very nature will not want to trust a self-signed certificate. Here is a utility that helps.

#### `mkcert`

- [Install mkcert](https://github.com/filosottile/mkcert)
    - [For Windows](https://github.com/filosottile/mkcert?tab=readme-ov-file#windows)
    - [For Linux](https://github.com/filosottile/mkcert?tab=readme-ov-file#linux)


> Currently not available via `winget` but an [issue is open](https://github.com/FiloSottile/mkcert/issues/400) and hopefully it will be added at some point.

Create a new folder inside your Angular client's root folder called `ssl`.

```
mkdir ssl
cd .\ssl\
```
Now run this command to create a local CA in the system trust store.

```
mkcert -install
```

Finally create a new certificate for the `localhost` domain.

```
mkcert localhost
```

If successful, the terminal should display something like:
```
Created a new certificate valid for the following names 📜
 - "localhost"

The certificate is at "./localhost.pem" and the key at "./localhost-key.pem" ✅

It will expire on 6 October 2026 🗓
```

Now to tell Angular about these files. This is done within the `angular.json` file.

```json
{
  "projects": {
    "client": {
      "architect": {
        "serve": {
          "options": {
            "ssl": true,
            "sslCert": "./ssl/localhost.pem",
            "sslKey": "./ssl/localhost-key.pem"
          }
        }
      }
    }
  }
}
```

When you restart Angular, you'll see its served on the `https` port (443):

```
Watch mode enabled. Watching for file changes...
  ➜  Local:   https://localhost:4200/
  ➜  press h + enter to show help
```
If your certificate expires, simply rerun `mkcert localhost`.

### Self-signed certificate (legacy)

Code that allows you to generate a new certificate with instructions are located at `git@github.com:bobbyache/generate-certificate-for-https.git`

Look in your client/ssl directory to find the certificates.

Windows 10

	1. Double click on the certificate (server.crt)
	2. Click on the button “Install Certificate …”
	3. Select whether you want to store it on user level or on machine level
	4. Click “Next”
	5. Select “Place all certificates in the following store”
	6. Click “Browse”
	7. Select “Trusted Root Certification Authorities”
	8. Click “Ok”
	9. Click “Next”
	10. Click “Finish”

If you get a prompt, click “Yes”

# Use HTTPS for the Server

When running requests to the Server you will run ito a `net::ERR_CERT_AUTHORITY_INVALID` response which shows up in your console every time you run a request against the server. You'll probably also see a "Your connection is not private" page with a "Proceed to localhost (unsafe)" link.

You can install a dev certificate by running the following commands in your api root folder.
```
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

At this point everything should work correctly and you should not get the invalid certifate error. Also when your API loads the swagger page you shouldn't get the warning page and clicking on the lock icon in the address bar should tell you that the connection is valid. Drilling in here should show that the certificate is valid.

- [ ] How do I reinstate an expire certificate?

# Use HTTPS with Postman

Postman cannot validate our self-signed certificate as it is "self signed". By default Postman wants to verify certificates over HTTPS. To get around this, in Postman go into Settings -> General and turn off SSL Certificate Verification.