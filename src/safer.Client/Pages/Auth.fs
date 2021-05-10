module safer.Client.Pages.Auth


open Fable.Core.JS
open Feliz
open Feliz.Bulma
open Feliz.UseDeferred
open safer.Client
open Fable.Import
open Fable.OidcClient
open Fable.Core.JsInterop
open safer.Client.AuthContext


[<ReactComponent>]
let IndexView () =

    let mgr = React.useContext(authContext)

    Html.div [
        Html.div "Auth page"
        Html.button [
            prop.text "login"
            prop.onClick (fun _ ->
                mgr.signinRedirect() |> ignore
            )
        ]
        Html.button [
            prop.text "user"
            prop.onClick (fun _ ->
                promise {
                    let! user = mgr.getUser()
                    match user with
                    | Some u ->
                        console.log(sprintf "%A" u.profile.name)
                    | None -> console.log("no user")
                } |> ignore
            )
        ]
    ]