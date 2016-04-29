open System
open System.IO
open FSharp.Charting
open FSharp.Data
open Suave
open Suave.Http.HttpRequest
open Suave.Http.HttpResult
open Suave.Web

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__



let ctx = FSharp.Data.WorldBankData.GetDataContext()

let data = ctx.Countries.``United States``.Indicators.``Poverty gap at $1.90 a day (2011 PPP) (%)``





[<EntryPoint>]
let main argv = 
    startWebServer defaultConfig (Successful.OK(sprintf "Hello world! Here's some data: %A" data.Code))
    
    0 // return an integer exit code

