module safer.Client.AuthContext

open Fable.OidcClient
open Fable.Core.JsInterop
open Feliz

let authServerSettings : UserManagerSettings =
    !!{|
        authority = Some "https://login.microsoftonline.com/48a5a247-ba65-4fff-b0f8-c2f843f4766b"
        client_id = Some "9d98197a-ad1e-465f-a62f-9dd7363edd7d"
        redirect_uri = Some "http://localhost:8080/auth-redirect"
        response_type = Some "code"
        scope = Some "openid profile scope1"
        post_logout_redirect_uri = Some "http://localhost:8080"

        filterProtocolClaims = Some true
        loadUserInfo = Some false
    |}

let private mgr:UserManager = Oidc.UserManager.Create authServerSettings


let authContext = React.createContext("auth", defaultValue = mgr)

let AuthContext ctx (children:ReactElement) =
    React.contextProvider(authContext, ctx, React.fragment [ children ])