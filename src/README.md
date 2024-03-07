# POC BFF “No tokens in the browser”


`OpenID Connect` is an authentication protocol, built on top of OAuth 2.0, that can be used to securely sign users in to web applications.

`OpenID Connect` extends the `OAuth 2.0` authorization protocol for use as an authentication protocol. This authentication protocol allows you to perform single sign-on. It introduces the concept of an ID token, which allows the client to verify the identity of the user and obtain basic profile information about the user.

`OpenID Connect` also enables applications to securely acquire `access tokens`. You can use `access tokens` to access resources that the authorization server secures.



## References

- [OAuth 2.0 for Browser-Based Apps](https://datatracker.ietf.org/doc/html/draft-ietf-oauth-browser-based-apps)
  - This demo is exemplifies *JavaScript Applications with a Backend* architecture
- [Web sign in with OpenID Connect in Azure Active Directory B2C](https://learn.microsoft.com/en-us/azure/active-directory-b2c/openid-connect)
- [Cookie spec - The "__Host-" Prefix](https://datatracker.ietf.org/doc/html/draft-ietf-httpbis-rfc6265bis#section-4.1.3.2)
- [IETF Spec- OAuth 2.0 Token Revocation](https://datatracker.ietf.org/doc/html/rfc7009#section-2)
- [Example of Custom Claims Transformation](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/claims?view=aspnetcore-8.0)
- [Cookies definitions for Azure AD B2C](https://learn.microsoft.com/en-us/azure/active-directory-b2c/cookie-definitions)
- [Configure session behavior in Azure Active Directory B2C](https://learn.microsoft.com/en-us/azure/active-directory-b2c/session-behavior?pivots=b2c-user-flow)

### VS for MacOS

- *Something is already running on port*:
```sh
lsof -i tcp:44447
npx kill-port 44447
```

- *Preserve PATH*:

```sh
/Applications/Visual\ Studio.app/Contents/MacOS/VisualStudio &
```

### Docker images

- https://hub.docker.com/_/microsoft-dotnet-sdk
- https://hub.docker.com/_/node/
- https://mcr.microsoft.com/en-us/product/dotnet/sdk/tags