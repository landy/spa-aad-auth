module safer.Client.Router

open Browser.Types
open Fable.Core.JS
open Feliz.Router
open Fable.Core.JsInterop

type Page =
    | Index
    | About
    | Auth
    | AuthRedirect

[<RequireQualifiedAccess>]
module Page =
    let defaultPage = Page.Index

    let parseFromUrlSegments segments =
        console.log(sprintf "%A" segments)
        match segments with
        | [ "about" ] -> Page.About
        | [ "auth-redirect"; _ ] -> Page.AuthRedirect
        | [ "auth" ] -> Page.Auth
        | [ ] -> Page.Index
        | _ -> defaultPage

    let noQueryString segments : string list * (string * string) list = segments, []

    let toUrlSegments = function
        | Page.Index -> [ ] |> noQueryString
        | Page.AuthRedirect -> [ "auth-redirect" ] |> noQueryString
        | Page.Auth -> [ "auth" ] |> noQueryString
        | Page.About -> [ "about" ] |> noQueryString

[<RequireQualifiedAccess>]
module Router =
    let goToUrl (e:MouseEvent) =
        e.preventDefault()
        let href : string = !!e.currentTarget?attributes?href?value
        Router.navigatePath href

    let navigatePage (p:Page) = p |> Page.toUrlSegments |> Router.navigatePath