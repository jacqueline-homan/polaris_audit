namespace Polaris

open Polaris.Core.Types

// This module is responsible for preparing
// a summary of a completed Call.
module CallSummary =

    // extract the caller from the call 
    let callerSummary (call:Call) =
        match call with
        | Call(caller,_,_) ->
            match caller with
            | Survivor -> "You are a survivor"
            | Advocate -> "You are an advocate"

    // extract the unmet needs from the Call
    let originalNeeds (Call(_, callerRequest, _)) =
        match callerRequest with
        | SurvivorAssistance (unmetNeeds) -> 
            unmetNeeds
            |> Set.map (fun need -> string need)
            |> Set.toList
        | _ -> [ "No unmet needs" ]
    
    let createReport (call:Call) =

        printfn "****************************************"
        printfn "*  From the CallSummary.fs file        *"
        printfn "* Caller Record of Help Requested      *"
        printfn "****************************************"

        callerSummary call |> printfn "%s"

        originalNeeds call 
        |> List.iter (printfn "%s")

        printfn "End of summary"