module safer.Client.View

open Fable.Core.JS
open Feliz
open Router
open SharedView
open safer.Client.AuthManager


[<ReactComponent>]
let AppView () =
    let page,setPage = React.useState(Router.currentPath() |> Page.parseFromUrlSegments)

    // routing for full refreshed page (to fix wrong urls)
    React.useEffectOnce (fun _ -> Router.navigatePage page)

    let navigation =
        Html.div [
            Html.a("Home", Page.Index)
            Html.span " | "
            Html.a("About", Page.About)
            Html.span " | "
            Html.a("Auth", Page.Auth)
        ]
    let render =
        match page with
        | Page.Index -> Pages.Index.IndexView ()
        | Page.Auth -> Pages.Auth.IndexView ()
        | Page.AuthRedirect ->
            console.log("test")
            promise {
                console.log "mgr.signinRedirectCallback()"
                let! user = mgr.signinRedirectCallback()
                console.log (sprintf "user: %A" user)
            } |> ignore
            Html.div "user"

        | Page.About -> Html.text "SAFEr Template"
    React.router [
        router.pathMode
        router.onUrlChanged (Page.parseFromUrlSegments >> setPage)
        router.children [ navigation; render ]
    ]