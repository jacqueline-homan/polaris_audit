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
    
    // list of NGOs contacted

    // from a referral, return the next referral
    // if it exists, otherwise None if the end of
    // the call chain has been reached.
    let nextReferral (referral:CallerRefToOtherNgo) : CallerRefToOtherNgo Option =
        match referral with
        | CallerRefToOtherNgo(followup, _) ->
            let help =
                match followup with
                | NotFollowedUp(help) -> help
                | FollowedUp(help) -> help
            match help with
            // here we care only about cases which
            // result in a referral
            | PartiallyHelped (_, nextReferral, _) -> Some(nextReferral)
            | Referred(nextReferral, _) -> Some(nextReferral)
            | _ -> None

    // pull out the NGO from a referral.
    // this can be simplified by pattern matching
    // in place.
    let extractNgo (referral:CallerRefToOtherNgo) : Ngo =
        match referral with
        | CallerRefToOtherNgo(_, ngo) -> ngo
    
    let rec extractNgos (ngos:Ngo list) (referral:CallerRefToOtherNgo) =
        // find NGO for current referral
        let ngo = extractNgo referral
        // append it to the list of NGOS
        let ngos = ngo :: ngos
        // explore next level of referral,
        // if there is one.
        let nextLevel = nextReferral referral
        match nextLevel with
        | None -> 
            // we have no referral after this one;
            // we can return all ngos we found in
            // the call chain.
            ngos
        | Some(nextReferral) -> 
            // there is another referral next: we
            // need to keep following call chain.
            extractNgos ngos nextReferral

    // extract a list of all the NGOS that are
    // involved in a Call
    let ngosInvolved (Call(caller, request, outcome)) =
        // caller = who the person is
        // request = what the person wanted
        // outcome : CallOutcome = the chain of interventions
        match outcome with
        // if we have a referral, extract the NGOs,
        // starting with an empty list.
        | CallerRef(ngoReferral, _) -> 
            extractNgos [] ngoReferral
        // otherwise no NGO is involved: 
        // we return an empty list.
        | _ -> []

    let displayNgo (Ngo(ngoType, ngoName)) =
        sprintf "%s (%A)" ngoName ngoType
 
    let ngosSummary (call:Call) =
        let ngos = ngosInvolved call
        ngos
        |> List.map displayNgo
        |> String.concat "\n"

    // full call report:
    // extract all the relevant data from the call,
    // and format / display appropriately
    let createReport (call:Call) =

        printfn "****************************************"
        printfn "*  From the CallSummary.fs file        *"
        printfn "* Caller Record of Help Requested      *"
        printfn "****************************************"

        callerSummary call |> printfn "%s"

        printfn "Unmet needs:"
        originalNeeds call 
        |> List.iter (printfn "%s")

        printfn "NGOs contacted:"
        ngosSummary call |> printfn "%s"


        printfn "End of summary"