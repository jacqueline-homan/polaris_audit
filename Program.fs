// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System
open System.IO
open Polaris.Types
open Polaris.TerminalBuilder



[<EntryPoint>]
let main argv = 
    let c = (caller())
    let cr = (callerRequest())
    let pa = (polaris_action())
    let ca = Polaris(c, cr, pa)
//    printfn "%A" argv
    0 // return an integer exit code

